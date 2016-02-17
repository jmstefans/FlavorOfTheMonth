using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using Fotm.Server.Database;
using Fotm.Server.Database.DataProvider;
using Fotm.Server.JobScheduling;
using Fotm.Server.Models.Base;
using Fotm.Server.JobScheduling.Jobs;
using Fotm.Server.WowAPI;
using Quartz;
using WowDotNetAPI.Models;

namespace Fotm.Server
{
    public class MainViewModel : ObservableObjectBase
    {
        #region Members

        // Connection info
        private const string SERVER = ".";
        private const string DB_NAME = "fotm";

        // Managers
        private readonly DbManager _dbManager = DbManager.Default;
        private JobSchedulingManager _jobManager = JobSchedulingManager.Default;

        #endregion

        #region Constructor

        public MainViewModel()
        {
            Initialize();
        }

        #endregion

        #region Public Methods

        public void InitializeCommandBindings(Window window)
        {

        }

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
            _jobManager.ScheduleRatingChangeJob();
        }

        #endregion

    }
}
