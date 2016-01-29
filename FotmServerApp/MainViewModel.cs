using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FotmServerApp.Base;
using FotmServerApp.Database;
using FotmServerApp.Database.DataProvider;
using FotmServerApp.WowAPI;

namespace FotmServerApp
{
    public class MainViewModel : ObservableObjectBase
    {
        #region Members

        // Connection info
        private const string SERVER = "NOYES";
        private const string DB_NAME = "FotmDb";

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

            var stuff = WowAPIManager.GetPvpStats();
            _dbManager.InsertObjects(stuff);
        }

        #endregion

    }
}
