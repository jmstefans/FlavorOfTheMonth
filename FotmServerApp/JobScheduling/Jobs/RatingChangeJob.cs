using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using FotmServerApp.Analysis.Algorithms;
using FotmServerApp.Models;
using FotmServerApp.WowAPI;
using Quartz;
using WowDotNetAPI.Models;

namespace FotmServerApp.JobScheduling.Jobs
{
    public class RatingChangeJob : IJob
    {
        #region Variables/Properties

        private static bool _setBaseLine = true;
        private static ConcurrentBag<PvpStats> _baseLineStats = new ConcurrentBag<PvpStats>();

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
                                .WithInterval(TimeSpan.FromMilliseconds(1))
                                .RepeatForever())
                                .Build();
                return _defaultTrigger;
            }
        }
        private static ITrigger _defaultTrigger;

        #endregion


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
        /// Executes the job.
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IJobExecutionContext context)
        {
            var stats = WowAPIManager.Default.GetPvpStats().ToList();
            if (_setBaseLine) // only do once on initial execute
            {
                _baseLineStats = new ConcurrentBag<PvpStats>(stats);
                _setBaseLine = false;
                return;
            }

            var allyWinners = new List<TeamMember>();
            var allyLosers = new List<TeamMember>(); // faction = 0
            var hordeWinners = new List<TeamMember>();
            var hordeLosers = new List<TeamMember>(); // factionId = 1

            // sort by faction and into winners and losers
            foreach (var stat in stats)
            {
                var baseStat = _baseLineStats.FirstOrDefault(b => b.Name.Equals(stat.Name) &&
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
            _baseLineStats = new ConcurrentBag<PvpStats>(stats);

            // ensure that each group has enough players to fill at least 1 team
            var bracket = (Bracket)context.JobDetail.JobDataMap[BRACKET_KEY];
            var teamSize = GetTeamSize(bracket);

            if (allyWinners.Count >= teamSize)
                ExecuteClustering(allyWinners, teamSize);

            if (allyLosers.Count >= teamSize)
                ExecuteClustering(allyLosers, teamSize);

            if (hordeWinners.Count >= teamSize)
                ExecuteClustering(hordeWinners, teamSize);

            if (hordeLosers.Count >= teamSize)
                ExecuteClustering(hordeLosers, teamSize);
        }

        private void ExecuteClustering(List<TeamMember> membersToCluster, int teamCount)
        {
            var teams = LeaderboardKmeans.ClusterTeams(membersToCluster, teamCount);
            // TODO: add to another thread queue and insert into db
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
    }
}
