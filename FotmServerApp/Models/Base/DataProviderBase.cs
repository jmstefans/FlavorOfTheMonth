using System;
using System.Data;

namespace FotmServerApp.Models.Base
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

        protected DataProviderBase(params string[] connectionProperties) // TODO: refactor this 
        {
            ConnectionString = GetFormattedConnectionString(connectionProperties);
        }

        #endregion

        #region Public Methods

        public abstract IDbConnection GetDataProviderConnection();

        public abstract string GetFormattedConnectionString(params string[] connectionProperties);

        #endregion

    }
}
