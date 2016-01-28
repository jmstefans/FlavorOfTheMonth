using System;
using System.Data;
using System.Data.SQLite;

namespace FotmServerApp.Database.DataProvider
{
    public class SqlDataProvider : DataProviderBase
    {
        public SqlDataProvider(string connectionString) : base(connectionString)
        {
        }

        public override IDbConnection GetDataProviderConnection()
        {
            throw new NotImplementedException();
        }

        public override string GetFormattedConnectionString(string dataSource)
        {
            throw new NotImplementedException();
        }
    }
}
