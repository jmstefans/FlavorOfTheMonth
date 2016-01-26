using System;
using System.Data;

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
            throw new NotImplementedException();
        }

        public override IDbDataAdapter GetDataProviderDataAdapter()
        {
            throw new NotImplementedException();
        }
    }
}
