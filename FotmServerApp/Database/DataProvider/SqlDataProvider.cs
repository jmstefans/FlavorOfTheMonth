using System;
using System.Data;

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

        public override IDbDataAdapter GetDataProviderDataAdapter()
        {
            throw new NotImplementedException();
        }
    }
}
