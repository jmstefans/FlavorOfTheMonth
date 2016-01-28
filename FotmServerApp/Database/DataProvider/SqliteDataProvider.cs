using System;
using System.Data;
using System.Data.SQLite;
using FotmServerApp.Database.Util;

namespace FotmServerApp.Database.DataProvider
{
    /// <summary>
    /// Data access layer for a SQLite database provider.
    /// </summary>
    public class SqliteDataProvider : DataProviderBase
    {
        public SqliteDataProvider(string connectionString) : base(connectionString)
        {
        }

        public override IDbConnection GetDataProviderConnection()
        {
            return new SQLiteConnection(ConnectionString);
        }

        public override string GetFormattedConnectionString(string dataSource)
        {
            return ConnectionStringBuilderUtil.CreateSqliteConnectionString(dataSource);
        }
    }
}
