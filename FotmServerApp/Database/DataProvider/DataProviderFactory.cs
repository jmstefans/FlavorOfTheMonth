using System;
using Fotm.Server.Models.Base;

namespace Fotm.Server.Database.DataProvider
{
    /// <summary>
    /// Factory pattern for creating a DataProvider.
    /// </summary>
    public class DataProviderFactory
    {
        /// <summary>
        /// Supported DataProviders.
        /// </summary>
        public enum DataProviderType
        {
            Sql, 
            Sqlite
        }

        /// <summary>
        /// Gets the corresponding DataProvider of the type passed in.
        /// </summary>
        /// <param name="dataProviderType">The type of DataProvider requested.</param>
        /// <param name="connectionProperties">The string properties used to create a connection string.</param>
        /// <returns>DataProvider of corresponding type.</returns>
        public static DataProviderBase GetDataProvider(DataProviderType dataProviderType, params string[] connectionProperties)
        {
            switch (dataProviderType)
            {
                case DataProviderType.Sql:
                    return new SqlDataProvider(connectionProperties);
                case DataProviderType.Sqlite:
                    return new SqliteDataProvider(connectionProperties);
                default:
                    throw new ArgumentException("Invalid DataProviderType");
            }
        }
    }
}
