using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Fotm.DAL;
using Fotm.DAL.Database;
using Fotm.DAL.Util;
using Fotm.Server.Analysis.Algorithms;
using Fotm.Server.WowAPI;
using Quartz;
using WowDotNetAPI.Models;

namespace Fotm.Server.JobScheduling.Jobs
{
    /// <summary>
    /// Job to query the PvpStats records of the WoWAPI, 
    /// sort and cluster into teams, 
    /// and insert successful teams into the database.
    /// </summary>
    public class LeaderboardClusteringJob
    : IJob
    {
        #region Variables

        // Used to define the baseline stats
        private static bool SetBaseLine
        {
            get
            {
                bool result;
                lock (_baseLock)
                {
                    result = _setBaseLine;
                }
                return result;
            }
            set
            {
                lock (_baseLock)
                {
                    _setBaseLine = value;
                }
            }
        }
        private static bool _setBaseLine = true;
        private static object _baseLock = new object();
        private static ConcurrentBag<PvpStats> _baseLineBag = new ConcurrentBag<PvpStats>();

        // Database manager
        private DbManager DbManager
        {
            get
            {
                DbManager result = null;
                lock (_dbLock)
                {
                    result = _dbManager;
                }
                return result;
            }
        }
        private DbManager _dbManager = DbManager.Default;
        private static object _dbLock = new object();

        #endregion

        #region Properties

        /// <summary>
        /// Default trigger with an interval of 1 sec that repeats for ever.
        /// </summary>
        public static ITrigger DefaultTrigger
        {
            get
            {
                if (_defaultTrigger != null) return _defaultTrigger;

                _defaultTrigger =
                    TriggerBuilder.Create()
                        .StartNow()
                        .WithSimpleSchedule(
                            s => s
                                .WithInterval(TimeSpan.FromSeconds(1))
                                .RepeatForever())
                                .Build();
                return _defaultTrigger;
            }
        }
        private static ITrigger _defaultTrigger;

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the job arguments used by the RatingChangeJob execution. 
        /// Call this before running the job.
        /// </summary>
        /// <param name="bracket">The bracket to query API for.</param>
        /// <returns>Dictionary populated with job keys and properties for execution.</returns>
        public static Dictionary<string, object> GetRatingChangeJobArguments(Bracket bracket)
        {
            return new Dictionary<string, object> { { BRACKET_KEY, bracket } };
        }
        private const string BRACKET_KEY = "bracketKey";

        private static object _statsLock = new object();

        /// <summary>
        /// Executes the sort, cluster, and db requests of WoW leaderboard API calls. 
        /// IJobs are created and handled via the Quartz Scheduler, both are managed by the <see cref="JobSchedulingManager"/>
        /// </summary>
        /// <param name="context">Context passed in by Scheduler.</param>
        public void Execute(IJobExecutionContext context)
        //public void Execute(Dictionary<string, Bracket> jobArgs)
        {
            LoggingUtil.LogMessage(DateTime.Now, "Executing RatingChange API call...", LoggingUtil.LogType.Notice);

            var stats = WowAPIManager.Default.GetPvpStats().ToList();

            lock (_statsLock)
            {
                if (SetBaseLine) // only set baseline on initial execute
                {
                    // TODO: can this be moved to the constructor of this job?
                    _baseLineBag = new ConcurrentBag<PvpStats>(stats);
                    SetBaseLine = false;
                    return;
                }
            }

            var allyWinners = new List<TeamMember>();
            var allyLosers = new List<TeamMember>(); // faction = 0
            var hordeWinners = new List<TeamMember>();
            var hordeLosers = new List<TeamMember>(); // factionId = 1

            lock (_statsLock) // need to lock here for consistent base line stats
            {
                // sort by faction and into winners and losers
                foreach (var stat in stats)
                {
                    var baseStat = _baseLineBag.FirstOrDefault(b => b.Name.Equals(stat.Name) &&
                                                                    b.RealmSlug.Equals(stat.RealmSlug));
                    if (baseStat == null)
                        continue; // player isn't in the baseline, nothing to compare against

                    var ratingChange = stat.Rating - baseStat.Rating;
                    if (ratingChange == 0) continue; // no rating change, ignore

                    var character = GetCharacter(stat);
                    var teamMember = new TeamMember
                    {
                        RatingChangeValue = ratingChange,
                        CurrentRating = stat.Rating,
                        CharacterID = character.CharacterID,
                        SpecID = character.SpecID,
                        RaceID = character.RaceID,
                        FactionID = character.FactionID,
                        GenderID = character.GenderID,
                        ModifiedDate = DateTime.Now,
                        ModifiedStatus = "I",
                        ModifiedUserID = 0,
                    };

                    var isAlly = stat.FactionId == 0;
                    var wonGame = ratingChange > 0;

                    if (isAlly)
                    {
                        if (wonGame)
                            allyWinners.Add(teamMember);
                        else
                            allyLosers.Add(teamMember);
                    }
                    else // horde
                    {
                        if (wonGame)
                            hordeWinners.Add(teamMember);
                        else
                            hordeLosers.Add(teamMember);
                    }
                }

                // current stats are baseline for next pass
                _baseLineBag = new ConcurrentBag<PvpStats>(stats);
            }

            // ensure that each group has enough players to fill at least 1 team
            var bracket = (Bracket)context.JobDetail.JobDataMap[BRACKET_KEY];
            // TESTING
            //var bracket = jobArgs[BRACKET_KEY];
            var teamSize = GetTeamSize(bracket);

            if (allyWinners.Count >= teamSize)
                ClusterAndInsertDb(allyWinners, teamSize, bracket);

            if (allyLosers.Count >= teamSize)
                ClusterAndInsertDb(allyLosers, teamSize, bracket);

            if (hordeWinners.Count >= teamSize)
                ClusterAndInsertDb(hordeWinners, teamSize, bracket);

            if (hordeLosers.Count >= teamSize)
                ClusterAndInsertDb(hordeLosers, teamSize, bracket);
        }

        #endregion

        #region Private Methods

        private DAL.Character GetCharacter(PvpStats pvpStats)
        {
            // TODO: still need to update the character on each pass

            // have to use the realm ID from the DB, not the pvp stats object
            DAL.Character character = null;
            var realm = DbManager.GetRealmByName(pvpStats.RealmName);
            if (realm != null)
                character = DbManager.GetCharacter(pvpStats.Name, realm.RealmID);

            if (character == null)
            {
                // no matching character, insert into db
                DbManager.InsertCharacter(pvpStats);
                // realm id will have been resolved after character insert
                realm = DbManager.GetRealmByName(pvpStats.RealmName);
                // refetch after insert
                character = DbManager.GetCharacter(pvpStats.Name, realm.RealmID);
            }

            if (character == null)
                throw new ArgumentException(nameof(character)); // if happens, something failed at db layer

            // need to update or insert the pvp stat on each pass
            var dbPvpStat = DbManager.GetPvpStatByCharacterId(character.CharacterID);
            if (dbPvpStat == null)
                DbManager.InsertPvpStats(pvpStats, character.CharacterID);
            else
                DbManager.UpdatePvpStats(pvpStats, dbPvpStat);

            return character;
        }

        private void ClusterAndInsertDb(List<TeamMember> membersToCluster, int teamSize, Bracket bracket)
        {
            LoggingUtil.LogMessage(DateTime.Now, "Executing team cluster...", LoggingUtil.LogType.Notice);

            var teams = LeaderboardKmeans.ClusterTeams(membersToCluster, teamSize);
            if (teams == null) return;

            foreach (var team in teams)
            {
                team.Bracket = bracket.ToString();
                team.ModifiedDate = DateTime.Now;
                team.ModifiedStatus = "I";
                team.ModifiedUserID = 0;
            }

            DbManager.InsertTeamsAndMembers(teams);
        }

        private int GetTeamSize(Bracket bracket)
        {
            switch (bracket)
            {
                case Bracket._2v2:
                    return 2;
                case Bracket._3v3:
                    return 3;
                case Bracket._5v5:
                    return 5;
                default:
                    throw new ArgumentOutOfRangeException(nameof(bracket));
            }
        }

        #endregion
    }
}
