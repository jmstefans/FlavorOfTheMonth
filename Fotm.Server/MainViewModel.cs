using System.Threading.Tasks;
using Fotm.DAL.Database;
using Fotm.DAL.Database.DataProvider;
using Fotm.DAL.Models.Base;
using Fotm.Server.JobScheduling;
using Fotm.DAL.Util;
using Fotm.Server.UI;
using WowDotNetAPI.Models;

namespace Fotm.Server
{
    public class MainViewModel : ObservableObjectBase
    {
        #region Vars/Props

        private const string DB_NAME = "fotm";

        public string Server
        {
            get
            {
                if (string.IsNullOrEmpty(_server))
                    _server = ConfigUtil.SQL_Server;
                return _server;
            }
            set
            {
                if (_server == value) return;

                _server = value;
                OnPropertyChanged(Server);
                CanStartJobs();
            }
        }
        private string _server;

        public string ApiKey
        {
            get
            {
                if (string.IsNullOrEmpty(_apiKey))
                    _apiKey = ConfigUtil.API_Key;
                return _apiKey;
            }
            set
            {
                if (_apiKey == value) return;

                _apiKey = value;
                OnPropertyChanged(ApiKey);
                CanStartJobs();
            }

        }
        private string _apiKey;

        // Managers
        private readonly DbManager _dbManager = DbManager.Default;
        private JobSchedulingManager _jobManager = JobSchedulingManager.Default;

        // Commands
        public RelayCommand SetDataSourceCommand
        {
            get
            {
                return _setDataSourceCommand ??
                       (_setDataSourceCommand = new RelayCommand(
                           a => SetDataSource(),
                           p => CanSetDataSource()));
            }
        }
        private RelayCommand _setDataSourceCommand;


        public RelayCommand StartJobsCommand
        {
            get
            {
                return _startJobsCommand ??
                       (_startJobsCommand = new RelayCommand(
                           a => StartJobs(),
                           p => CanStartJobs()));
            }
        }
        private RelayCommand _startJobsCommand;

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
            _jobManager.Dispose();
        }

        #endregion

        #region Private Methods

        private void Initialize()
        {
            //if (CanSetDataSource())
            //{
            //    _dbManager.SetDataProvider(DataProviderFactory.DataProviderType.Sql, Server, DB_NAME);
            //}

            // Starting jobs on initializiation for now
            if (!IsValidConfig()) return; // TODO: err out here

            _dbManager.SetDataProvider(DataProviderFactory.DataProviderType.Sql, ConfigUtil.SQL_Server, DB_NAME);
            StartJobs();
            //StartJobsDebugAsync();
        }

        private bool CanSetDataSource()
        {
            return !string.IsNullOrEmpty(_apiKey) &&
                   !string.IsNullOrEmpty(_server);
        }

        private bool IsValidConfig()
        {
            return !string.IsNullOrEmpty(ConfigUtil.API_Key) &&
                   !string.IsNullOrEmpty(ConfigUtil.SQL_Server);
        }

        private void SetDataSource()
        {
            ConfigUtil.SetConfig(_apiKey, _server);
        }

        private bool CanStartJobs()
        {
            return true;
            // TODO: need to check for valid connection here
            return CanSetDataSource();
        }

        private void StartJobs()
        {
            _jobManager.ScheduleRatingChangeJob(bracket: Bracket._2v2, jobKey: "ratingChangeJob1");
            _jobManager.ScheduleRatingChangeJob(bracket: Bracket._3v3, jobKey: "ratingChangeJob2");
            _jobManager.ScheduleRatingChangeJob(bracket: Bracket._5v5, jobKey: "ratingChangeJob3");
            _jobManager.ScheduleRatingChangeJob(bracket: Bracket._2v2, region: WowDotNetAPI.Region.EU, jobKey: "ratingChangeJob4");
            _jobManager.ScheduleRatingChangeJob(bracket: Bracket._3v3, region: WowDotNetAPI.Region.EU, jobKey: "ratingChangeJob5");
            _jobManager.ScheduleRatingChangeJob(bracket: Bracket._5v5, region: WowDotNetAPI.Region.EU, jobKey: "ratingChangeJob6");
        }

        private async void StartJobsDebugAsync()
        {
            await Task.Run(() => _jobManager.ScheduleRatingChangeJobDebugging());
        }

        #endregion
    }
}
