using System;
using System.Data;
using System.Data.Entity.Infrastructure;
using Fotm.DAL.Models.Base;
using Fotm.DAL.Util;

namespace Fotm.DAL.Database.DataProvider
{
    public class SqlDataProvider : DataProviderBase
    {
        public SqlDataProvider(params string[] connectionProperties) : base(connectionProperties)
        {
        }

        public override IDbConnection GetDataProviderConnection()
        {
            lock (_dbLock)
            {
                return new SqlConnectionFactory().CreateConnection(ConnectionString);
            }
        }
        private object _dbLock = new object();

        public override string GetFormattedConnectionString(params string[] connectionProperties)
        {
            return "Server=tcp:fotmdb.database.windows.net,1433;Database=fotm;User ID=pandamic@fotmdb;Password=VObl15isdB7F511qdGQZ;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            //if (connectionProperties.Length != 2)
            //    throw new ArgumentException("Connection string requires server name and database name");

            //return ConnectionStringBuilderUtil.CreateSqlServerConnectionString(connectionProperties[0], connectionProperties[1]);
        }
    }
}
