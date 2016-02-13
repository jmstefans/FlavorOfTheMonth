using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FotmServerApp.Analysis.Algorithms;
using FotmServerApp.Database;
using FotmServerApp.Models;
using FotmServerApp.WowAPI;
using Quartz;
using WowDotNetAPI.Models;

namespace FotmServerApp.JobScheduling.Jobs
{
    public class RatingChangeJob 
        //: IJob
    {
        #region Variables

        // Used to define the baseline stats
        private static bool _setBaseLine = true;
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
        private static object _baseLock = new object();

        private static List<PvpStats> _baseLineStats = new List<PvpStats>();
        private static List<PvpStats> BaselineStats
        {
            get
            {
                List<PvpStats> stats = null;
                lock (_lock)
                {
                    stats = _baseLineStats;
                }
                return stats;
            }
            set
            {
                lock (_lock)
                {
                    if (value != null)
                        _baseLineStats = value;
                }
            }
        }
        private static object _lock = new object();

        #endregion

        #region Properties

        /// <summary>
        /// Can be used as the default trigger for this job.
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
        /// <param name="bracket"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetRatingChangeJobArguments(Bracket bracket)
        {
            return new Dictionary<string, object> { { BRACKET_KEY, bracket } };
        }
        private const string BRACKET_KEY = "bracketKey";

        /// <summary>
        /// Executes the job, this is handled by the Scheduler in Quartz.
        /// </summary>
        /// <param name="context">Context passed in by Scheduler.</param>
        //public void Execute(IJobExecutionContext context)
        //{
        public void Execute(Dictionary<string, Bracket> test)
        { 
            Console.WriteLine($"{DateTime.Now}: Executing RatingChange API call...");

            var stats = WowAPIManager.Default.GetPvpStats().ToList();
            if (SetBaseLine) // only do once on initial execute
            {
                BaselineStats = new List<PvpStats>(stats);
                SetBaseLine = false;
                return;
            }

            var allyWinners = new List<TeamMember>();
            var allyLosers = new List<TeamMember>(); // faction = 0
            var hordeWinners = new List<TeamMember>();
            var hordeLosers = new List<TeamMember>(); // factionId = 1

            // sort by faction and into winners and losers
            foreach (var stat in stats)
            {
                var baseStat = BaselineStats.FirstOrDefault(b => b.Name.Equals(stat.Name) &&
                                                                  b.RealmSlug.Equals(stat.RealmSlug));
                if (baseStat == null)
                    continue; // player isn't in the baseline, nothing to compare against

                var ratingChange = stat.Rating - baseStat.Rating;
                if (ratingChange == 0) continue; // no rating change, ignore

                var teamMember = new TeamMember
                {
                    Name = stat.Name,
                    RatingChangeValue = ratingChange,
                    RealmName = stat.RealmName,
                    Spec = stat.Spec
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
            BaselineStats = new List<PvpStats>(stats);

            // ensure that each group has enough players to fill at least 1 team
            //var bracket = (Bracket)context.JobDetail.JobDataMap[BRACKET_KEY];
            var bracket = (Bracket)test[BRACKET_KEY];
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

        private DbManager _dbManager = DbManager.Default;

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
        private static object _dbLock = new object();

        private void ClusterAndInsertDb(List<TeamMember> membersToCluster, int teamSize, Bracket bracket)
        {
            Console.Write($"{DateTime.Now}: Executing team cluster...");
            var teams = LeaderboardKmeans.ClusterTeams(membersToCluster, teamSize);
            if (teams == null) return; 

            foreach (var team in teams)
                team.PvpBracket = bracket;

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
                    throw new ArgumentOutOfRangeException("bracket");
            }
        }

        #endregion
    }
}
