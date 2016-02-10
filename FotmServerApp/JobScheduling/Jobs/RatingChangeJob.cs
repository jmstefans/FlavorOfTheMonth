using System;
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
        private static List<PvpStats> _baseLineStats = new List<PvpStats>();

        private static ITrigger _defaultTrigger;
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
                                .WithIntervalInSeconds(30)
                                .RepeatForever())
                                .Build();
                return _defaultTrigger;
            }
        }
        #endregion

        public void Execute(IJobExecutionContext context)
        {
            // CLUSTER TEST!    
            var stats = WowAPIManager.Default.GetPvpStats().ToList();
            if (_setBaseLine) // only do once on initial execute
            {
                _baseLineStats = stats;
                _setBaseLine = false;
                return;
            }

            var allyWinners = new List<LeaderboardKmeans.TeamMember>();
            foreach (var stat in stats)
            {
                if (stat.FactionId == 1) continue; 

                var baseStat = _baseLineStats.FirstOrDefault(b => b.Name.Equals(stat.Name) && 
                                                                  b.RealmSlug.Equals(stat.RealmSlug));
                if (baseStat == null) 
                    continue; // player isn't in the baseline, nothing to compare against

                var ratingChange = stat.Rating - baseStat.Rating;
                if (ratingChange <= 0) continue; 

                allyWinners.Add(new LeaderboardKmeans.TeamMember
                {
                    Name = stat.Name,
                    RatingChangeValue = ratingChange,
                    RealmName = stat.RealmName,
                    Spec = stat.Spec
                });
            }

            _baseLineStats.Clear();
            _baseLineStats = stats;

            var teams = new List<LeaderboardKmeans.Team>();
            if (allyWinners.Count > 3) // at least 1 cluster - TODO: update to size of team 
            {
                teams = LeaderboardKmeans.ClusterTeams(allyWinners, 3);

                // TODO: insert teams into DB

            }
        }
    }
}
