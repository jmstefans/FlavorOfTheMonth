using System;
using System.Data;

namespace FotmServerApp.Database.DataProvider
{
    /// <summary>
    /// Base class for a data access layer (DAL).
    /// </summary>
    public abstract class DataProviderBase 
    {
        #region Members

        private string _strConnectionString;

        #endregion

        #region Properties

        public string ConnectionString
        {
            get
            {
                if (_strConnectionString == string.Empty || _strConnectionString.Length == 0)
                    throw new ArgumentException("Invalid database connection string.");

                return _strConnectionString;
            }
            set
            { _strConnectionString = value; }
        }

        #endregion

        #region Constructor

        protected DataProviderBase(string connectionString)
        {
            ConnectionString = connectionString;
        }

        #endregion

        #region Public Methods

        public abstract IDbConnection GetDataProviderConnection();

        #endregion

    }
}
