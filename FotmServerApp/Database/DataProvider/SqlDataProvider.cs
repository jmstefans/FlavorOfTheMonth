using System;
using System.Data;
using System.Data.Entity.Infrastructure;
using FotmServerApp.Database.Util;

namespace FotmServerApp.Database.DataProvider
{
    public class SqlDataProvider : DataProviderBase
    {
        public SqlDataProvider(params string[] connectionProperties) : base(connectionProperties)
        {
        }

        public override IDbConnection GetDataProviderConnection()
        {
            return new SqlConnectionFactory().CreateConnection(ConnectionString);
        }

        public override string GetFormattedConnectionString(params string[] connectionProperties)
        {
            if (connectionProperties.Length != 2) 
                throw new ArgumentException("Connection string requires server name and database name");

            return ConnectionStringBuilderUtil.CreateSqlServerConnectionString(connectionProperties[0], connectionProperties[1]);
        }
    }
}
