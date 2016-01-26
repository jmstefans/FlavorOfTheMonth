using System;

namespace FotmServerApp.Database.DataProvider
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
        /// <param name="connectionString">The connection string used to connect the DataProvider.</param>
        /// <returns>DataProvider of corresponding type.</returns>
        public static DataProviderBase GetDataProvider(DataProviderType dataProviderType, string connectionString)
        {
            switch (dataProviderType)
            {
                case DataProviderType.Sql:
                    return new SqlDataProvider(connectionString);
                case DataProviderType.Sqlite:
                    return new SqliteDataProvider(connectionString);
                default:
                    throw new ArgumentException("Invalid DataProviderType");
            }
        }
    }
}
