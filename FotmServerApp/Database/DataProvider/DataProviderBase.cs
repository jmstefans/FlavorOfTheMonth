using System;
using System.Data;

namespace FotmServerApp.Database.DataProvider
{
    /// <summary>
    /// Base class for a data provider.
    /// </summary>
    public abstract class DataProviderBase 
    {
        #region Members

        private string _connectionString;

        #endregion

        #region Properties

        public string ConnectionString
        {
            get
            {
                if (_connectionString == string.Empty || _connectionString.Length == 0)
                    throw new ArgumentException("Invalid database connection string.");

                return _connectionString;
            }
            set
            { _connectionString = value; }
        }

        #endregion

        #region Constructor

        protected DataProviderBase(string dataSource)
        {
            ConnectionString = GetFormattedConnectionString(dataSource);
        }

        #endregion

        #region Public Methods

        public abstract IDbConnection GetDataProviderConnection();

        public abstract string GetFormattedConnectionString(string dataSource);

        #endregion

    }
}
