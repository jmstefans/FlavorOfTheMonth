using System;
using System.Data;
using FotmServerApp.Database.DataProvider;
using FotmServerApp.Database.Util;

namespace FotmServerApp.Database
{
    /// <summary>
    /// Persistent DB class for reading and writing to the database. 
    /// </summary>
    public class DbManager : IDisposable
    {
        #region Singleton De/Constructor & Instance

        private static DbManager _dbManager;

        public static DbManager Instance => _dbManager ?? (_dbManager = new DbManager());

        private DbManager() { }
        
        #endregion

        #region Members

        private DataProviderBase _dataProvider;
        private IDbConnection _dbConnection;

        #endregion

        #region Properties

        private IDbConnection DbConnection
        {
            get
            {
                if (_dbConnection == null)
                    throw new ArgumentException("DB connection cannot be null.");

                if (_dbConnection.State != ConnectionState.Open)
                    _dbConnection.Open(); 

                return _dbConnection;
            }
        }

        #endregion

        #region Data Provider / Connection

        /// <summary>
        /// Sets the active data provider.
        /// </summary>
        /// <param name="dataProviderType">The type of data provider <see cref="DataProviderFactory.DataProviderType"/> being set.</param>
        /// <param name="connectionString">Corresponding connection string for the data provider. 
        ///                                For connection string util <see cref="ConnectionStringBuilderUtil"/></param>
        public void SetDataProvider(DataProviderFactory.DataProviderType dataProviderType, string connectionString)
        {
            _dataProvider = DataProviderFactory.GetDataProvider(dataProviderType, connectionString);
            _dbConnection?.Dispose();
            _dbConnection = _dataProvider.GetDataProviderConnection();
        }

        #endregion

        #region CRUD

        #region Create

        #endregion

        #region Read

        #endregion

        #region Update

        #endregion

        #region Delete

        #endregion

        #endregion

        #region IDisposable

        public void Dispose()
        {
            _dbConnection?.Dispose();
        }

        #endregion
    }
}
