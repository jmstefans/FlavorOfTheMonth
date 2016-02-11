using System;
using System.Collections.Generic;
using FotmServerApp.JobScheduling.Jobs;
using FotmServerApp.Models.Base;
using Quartz;
using Quartz.Impl;

namespace FotmServerApp.JobScheduling
{
    /// <summary>
    /// Class for creating/managing scheduled tasks.
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
