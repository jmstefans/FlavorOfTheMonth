using System;
using System.Data;
using System.Data.SQLite;
using Fotm.DAL.Database.Util;
using Fotm.DAL.Models.Base;

namespace Fotm.DAL.Database.DataProvider
{
    /// <summary>
    /// Data access layer for a SQLite database provider.
    /// </summary>
    public class SqliteDataProvider : DataProviderBase
    {
        public SqliteDataProvider(params string[] connectionProperties) : base(connectionProperties)
        {
        }

        public override IDbConnection GetDataProviderConnection()
        {
            return new SQLiteConnection(ConnectionString);
        }

        public override string GetFormattedConnectionString(params string[] dataSource)
        {
            if (dataSource.Length > 1 || dataSource.Length <= 0)
                throw new ArgumentException("dataSource is invalid");
            return ConnectionStringBuilderUtil.CreateSqliteConnectionString(dataSource[0]);
        }
    }
}
