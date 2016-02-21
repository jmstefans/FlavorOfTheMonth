using System;
using System.Collections.Generic;
using Fotm.DAL.Models.Base;
using Fotm.Server.JobScheduling.Jobs;
using Quartz;
using Quartz.Impl;
using WowDotNetAPI.Models;

namespace Fotm.Server.JobScheduling
{
    /// <summary>
    /// Manager class for creating and scheduling of jobs.
    /// The JobSchedulingManager and IJobs that it create, 
    /// exchange information back and forth needed to complete a job.
    /// But access to IJobs is only handled through the JobSchedulingManager.
    /// </summary>
    public class JobSchedulingManager : ManagerBase<JobSchedulingManager>, IDisposable
    {
        #region Properties

        private IScheduler Scheduler
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
        private IScheduler _scheduler;

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
        public void ScheduleJob<T>(ITrigger trigger, string key, string group,
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
        /// <param name="trigger">The trigger for the job to execute. If null, default will be applied.</param>
        public void ScheduleRatingChangeJob(string jobKey = "ratingChangeJob",
                                            string groupKey = "ratingChangeGroup",
                                            Bracket bracket = Bracket._3v3,
                                            ITrigger trigger = null)
        {
            if (trigger == null)
                trigger = LeaderboardClusteringJob.DefaultTrigger;

            var jobArgs = LeaderboardClusteringJob.GetRatingChangeJobArguments(Bracket._3v3);

            // TESTING
            //var jobArgs = new Dictionary<string, Bracket> { { "bracketKey", Bracket._3v3 } };
            //var job = new LeaderboardClusteringJob();
            //while (true)
            //{
            //    job.Execute(jobArgs);
            //}

            ScheduleJob<LeaderboardClusteringJob>(trigger, jobKey, groupKey, jobArgs);
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            if (_scheduler == null) return;

            if (_scheduler.IsStarted)
                _scheduler.Shutdown();

            _scheduler = null;
        }

        #endregion
    }
}
