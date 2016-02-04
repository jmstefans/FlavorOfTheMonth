using System;
using FotmServerApp.JobScheduling.Jobs;
using Quartz;
using Quartz.Impl;

namespace FotmServerApp.JobScheduling
{
    /// <summary>
    /// Class for creating/managing scheduled tasks.
    /// </summary>
    public class JobSchedulingManager : IDisposable
    {
        #region Singleton Constructor

        private static JobSchedulingManager _instance;

        public static JobSchedulingManager Instance => _instance ?? (_instance = new JobSchedulingManager());

        private JobSchedulingManager() { }

        #endregion

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

        public void ScheduleJob<T>(ITrigger trigger, string key, string group = "") where T : IJob 
        {
            var jobDetail = JobBuilder.Create<T>()
                                      .WithIdentity(key, group)
                                      .Build();
        
            Scheduler.ScheduleJob(jobDetail, trigger);
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
