namespace FotmServerApp.Database.Util
{
    public class ConnectionStringBuilderUtil
    {
        /// <summary>
        /// Creates a basic SQLite connection string.
        /// </summary>
        /// <param name="dataSource">File path to the SQLite DB.</param>
        /// <param name="version">Version of the database, default is current (3).</param>
        /// <returns></returns>
        public static string CreateSqliteConnectionString(string dataSource, int version = 3)
        {
            return $"Data Source={dataSource}; Version={version};";
        }


        public static string CreateSqlServerConnectionString(string server, string database)
        {
            return $"Server={server};Database={database};Trusted_Connection=true;";
        }

    }
}
