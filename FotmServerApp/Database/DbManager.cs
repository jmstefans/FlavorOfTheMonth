using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Dapper;
using FotmServerApp.Database.DataProvider;
using FotmServerApp.Database.Util;
using WowDotNetAPI.Models;

namespace FotmServerApp.Database
{
    /// <summary>
    /// Persistent DB class for reading and writing to the database. 
    /// </summary>
    public class DbManager : IDisposable
    {
        #region Singleton Constructor & Instance

        private static DbManager _dbManager;

        public static DbManager Instance => _dbManager ?? (_dbManager = new DbManager());

        private DbManager()
        {
        }

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
        public void SetDataProvider(DataProviderFactory.DataProviderType dataProviderType, params string[] connectionProperties)
        {
            _dataProvider = DataProviderFactory.GetDataProvider(dataProviderType, connectionProperties);
            _dbConnection?.Dispose();
            _dbConnection = _dataProvider.GetDataProviderConnection();
        }

        #endregion

        #region CRUD

        #region Create

        public void InsertObjects<T>(IEnumerable<T> objects) where T : new()
        {
            var type = typeof(T);

            using (var trans = DbConnection.BeginTransaction())
            {
                try
                {
                    var cols = GetColumnNames<T>();
                    var colPar = GetColumnParameters(cols);

                    foreach (var pvp in objects)
                    {
                        var query =
                       $"insert into [{type.Name}] ({string.Join(",", cols)}) values ({string.Join(",", colPar)});";
                        DbConnection.Execute(query, pvp, trans);
                    }

                    trans.Commit();
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    Console.WriteLine("Failed: " + e);
                }
            }
        }

        #endregion

        #region Read

        #endregion

        #region Update

        #endregion

        #region Delete

        #endregion

        #endregion

        #region Helpers 

        private string[] GetColumnNames<T>() where T : new()
        {
            var properties = typeof(T).GetProperties();
            var columns = new string[properties.Length];

            for (var i = 0; i < properties.Length; i++)
            {
                columns[i] = ConvertToColumnName(properties[i].Name);
            }

            return columns;
        }

        private string[] GetColumnParameters(string[] columnNames)
        {
            return columnNames.Select(c => $"@{c}").ToArray();
        }

        private string ConvertToColumnName(string propertyName)
        {
            var txtInfo = new CultureInfo("en-US", false).TextInfo;
            return txtInfo.ToTitleCase(propertyName);
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            _dbConnection?.Dispose();
        }

        #endregion

    }
}
