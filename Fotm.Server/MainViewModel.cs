using System.Windows;
using Fotm.DAL.Database;
using Fotm.DAL.Database.DataProvider;
using Fotm.DAL.Models.Base;
using Fotm.Server.JobScheduling;
using Fotm.DAL.Util;
using WowDotNetAPI.Models;

namespace Fotm.Server
{
    public class MainViewModel : ObservableObjectBase
    {
        #region Members

        // Connection info
        private string SERVER = ConfigUtil.SQL_Server;
        private string DB_NAME = "fotm";

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
            _jobManager.Dispose();
        }

        #endregion

        #region Private Methods

        private void Initialize()
        {
            _dbManager.SetDataProvider(DataProviderFactory.DataProviderType.Sql, SERVER, DB_NAME);
            _jobManager.ScheduleRatingChangeJob(bracket:Bracket._2v2);
        }

        #endregion

    }
}
