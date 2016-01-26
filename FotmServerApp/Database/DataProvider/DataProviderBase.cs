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
        private IDbConnection _connection;

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

        public abstract IDbDataAdapter GetDataProviderDataAdapter();

        #endregion

        #region Database Transaction

        public string OpenConnection()
        {
            string response;
            try
            {
                _connection = GetDataProviderConnection();
                response = _connection.GetType().Name + " Open Successfully";
            }
            catch
            {
                _connection.Close();
                response = "Unable to Open " + _connection.GetType().Name;
            }
            return response;
        }

        #endregion
    }
}
