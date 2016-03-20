using System;
using System.Collections.Generic;
using Fotm.DAL.Models.Base;
using Fotm.Server.JobScheduling.Jobs;
using Quartz;
using Quartz.Impl;
using WowDotNetAPI;
using WowDotNetAPI.Models;

namespace Fotm.Server.JobScheduling
{
    /// <summary>
    /// Manager class for creating and scheduling of jobs.
    /// The JobSchedulingManager and IJobs that it create, 
    /// exchange information back and forth needed to complete a job.
    /// But access to IJobs is only handled through the JobSchedulingManager.
    /// </summary>
    public class JobSchedulingManager
    {
        #region Properties

        private static IScheduler Scheduler
        {
            get
            {
                if (_scheduler == null)
                    _scheduler = StdSchedulerFactory.GetDefaultScheduler();

                if (!_scheduler.IsStarted)
                    _scheduler.Start();

                return _scheduler;
            }
        }
        private static IScheduler _scheduler;

        #endregion

        #region Public

        /// <summary>
        /// Creates and schedules an IJob to run.
        /// </summary>
        /// <typeparam name="T">Type of IJob to schedule.</typeparam>
        /// <param name="trigger">The trigger to apply.</param>
        /// <param name="key">The job key.</param>
        /// <param name="group">The job group key.</param>
        /// <param name="jobArguments">Arguments (if any) needed for the job.</param>
        public static void ScheduleJob<T>(ITrigger trigger, string key, string group,
                                   IDictionary<string, object> jobArguments = null) where T : IJob
        {
            var jobDetail = JobBuilder.Create<T>()
                                      .WithIdentity(key, group);

            if (jobArguments != null)
            {
                jobDetail.SetJobData(new JobDataMap(jobArguments));
            }

            Scheduler.ScheduleJob(jobDetail.Build(), trigger);
        }

        /// <summary>
        /// Creates and schedules the RatingChangeJob that pulls pvp stats data, 
        /// performs clustering, then writes team results to the database.
        /// </summary>
        /// <param name="jobKey">The unique key for this job.</param>
        /// <param name="groupKey">The unique key for this group of jobs.</param>
        /// <param name="bracket">The bracket to pull.</param>
        /// <param name="region">The region to pull leaderboard from.</param>
        /// <param name="trigger">The trigger for the job to execute. If null, default will be applied.</param>
        public static void ScheduleRatingChangeJob(string jobKey = "ratingChangeJob",
                                            string groupKey = "ratingChangeGroup",
                                            Bracket bracket = Bracket._3v3,
                                            Region region = Region.US,
                                            ITrigger trigger = null)
        {
            if (trigger == null)
                trigger = LeaderboardClusteringJob.DefaultTrigger;

            var jobArgs = LeaderboardClusteringJob.GetRatingChangeJobArguments(bracket, region);
            ScheduleJob<LeaderboardClusteringJob>(trigger, jobKey, groupKey, jobArgs);
        }

        /// <summary>
        /// Used for debugging purposes only.
        /// </summary>
        public static void ScheduleRatingChangeJobDebugging()
        {
            var test = new LeaderboardClusteringJob();
            while (true)
            {
                test.ExecuteDebugging();
            }
        }

        public static void Cleanup()
        {
            if (_scheduler == null) return;

            if (_scheduler.IsStarted)
                _scheduler.Shutdown();

            _scheduler = null;
        }

        #endregion
    }
}
