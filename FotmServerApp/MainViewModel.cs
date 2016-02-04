using System;
using System.Collections.Generic;
using System.Threading;
using FotmServerApp.Database;
using FotmServerApp.Database.DataProvider;
using FotmServerApp.JobScheduling;
using FotmServerApp.JobScheduling.Jobs;
using FotmServerApp.Models.Base;
using FotmServerApp.WowAPI;
using Quartz;
using WowDotNetAPI.Models;

namespace FotmServerApp
{
    public class MainViewModel : ObservableObjectBase
    {
        #region Members

        // Connection info
        private const string SERVER = ".";
        private const string DB_NAME = "fotm";

        // Managers
        private readonly DbManager _dbManager = DbManager.Instance;
        private JobSchedulingManager _jobManager = JobSchedulingManager.Instance;


        #endregion

        #region Constructor

        public MainViewModel()
        {
            Initialize();
        }

        #endregion

        #region Public Methods

        public void CleanUp()
        {
            _dbManager.Dispose();
            _jobManager.Dispose();
        }

        #endregion

        #region Private Methods

        private void Initialize()
        {
            _dbManager.SetDataProvider(DataProviderFactory.DataProviderType.Sql, SERVER, DB_NAME);

            //var job = new RatingChangesJob();
            //_jobManager.ScheduleJob<RatingChangesJob>(job.DefaultTrigger, "RatingChangesJob", "RatingChangesGroup");
            
            // do some clusterfucking
            var clusterJob = new KmeansClusteringJob(DateTime.Now, DateTime.MaxValue);
            clusterJob.Execute(null);
            //_jobManager.ScheduleJob<KmeansClusteringJob>(clusterJob.DefaultTrigger, "KmeansClusteringJob", "KmeansClusteringGroup");
        }

        #endregion

    }
}
