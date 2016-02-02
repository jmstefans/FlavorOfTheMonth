﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
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
                       $"insert into [{type.Name}] ({string.Join(",", cols)}, ModifiedDate, ModifiedStatus, ModifiedUserID) values ({string.Join(",", colPar)}, '{DateTime.Now}', 'I', 0);";
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

        /// <summary>
        /// Inserts rows into the Character table if there are new leaderboard characters.
        /// Inserts and updates rows into the PvpStats table to keep it current with every characters' ratings.
        /// Inserts a new row into the RatingChange table with the rating difference and time of the change.
        /// We will cluster on the RatingChange table to look for teams.
        /// </summary>
        public void InsertRatingChanges(IEnumerable<PvpStats> objects)
        {
            using (var trans = DbConnection.BeginTransaction())
            {
                try
                {
                    foreach (var pvp in objects)
                    {
                        // Check to see if character is already in database
                        SqlCommand cmd = new SqlCommand($"select count(*) from Character where name like '{pvp.Name}' and server like '{pvp.RealmName}'");
                        bool isInDB = (int)cmd.ExecuteScalar() == 1;

                        // If character is not in database
                        if (!isInDB)
                        {
                            // Add character to Character table
                            SqlCommand cmd2 = new SqlCommand($"insert into Character values ({pvp.Name}, {pvp.RealmName}, '{DateTime.Now}', 'I', 0)");
                            cmd2.ExecuteNonQuery();

                            // get the id from query above and insert with pvp stats table

                            // Add character to PvpStats table
                            var cols = GetColumnNames<PvpStats>();
                            var colPar = GetColumnParameters(cols);
                            var query = $"insert into PvpStats values ({string.Join(",", colPar)}, '{DateTime.Now}', 'I', 0);";
                            DbConnection.Execute(query, pvp, trans);

                            // Don't need to add to RatingChange table just yet since we don't have a difference of rating yet
                        }
                        else // If character is in database
                        {
                            // Get character's id
                            uint characterID = (uint)new SqlCommand($"select top 1 characterid from character where name like '{pvp.Name}' and server like '{pvp.RealmName}'").ExecuteScalar();

                            // Update that character's PvpStats row with current values to keep it current
                            SqlCommand cmd3 = new SqlCommand($"update pvpstats set Rating = {pvp.Rating}, ModifiedDate = '{DateTime.Now}', ModifiedStatus = 'U' where ");

                            // Insert a row into the RatingChange table indicating the difference and current time

                        }


                        // var query =
                        //$"insert into [{type.Name}] ({string.Join(",", cols)}, ModifiedDate, ModifiedStatus, ModifiedUserID) values ({string.Join(",", colPar)}, '{DateTime.Now}', 'I', 0);";
                        // DbConnection.Execute(query, pvp, trans);
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

        #endregion Create

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
