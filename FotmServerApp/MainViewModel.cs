using System.Collections.Generic;
using System.Threading;
using FotmServerApp.Base;
using FotmServerApp.Database;
using FotmServerApp.Database.DataProvider;
using FotmServerApp.WowAPI;
using WowDotNetAPI.Models;

namespace FotmServerApp
{
    public class MainViewModel : ObservableObjectBase
    {
        #region Members

        // Connection info
        private const string SERVER = ".";
        private const string DB_NAME = "fotm";

        // Db
        private readonly DbManager _dbManager = DbManager.Instance;

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
        }

        #endregion

        #region Private Methods

        private void Initialize()
        {
            _dbManager.SetDataProvider(DataProviderFactory.DataProviderType.Sql, SERVER, DB_NAME);

            //var stuff = WowAPIManager.GetPvpStats();
            //_dbManager.InsertObjects(stuff);

            var stuff2 = WowAPIManager.GetPvpStats();
            IEnumerable<PvpStats> pvpstats = _dbManager.InsertRatingChanges(stuff2);

            // do some clusterfucking


            Thread.Sleep(120 * 1000);

            var stuff3 = WowAPIManager.GetPvpStats();
            _dbManager.InsertRatingChanges(stuff3);
        }

        #endregion

    }
}
