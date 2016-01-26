namespace FotmServerApp.Database.Util
{
    public class ConnectionStringBuilderUtil
    {
        public static string CreateSqliteConnectionString(string dataSource, int version = 3)
        {
            return $"Data Source={dataSource}; Version={version};";
        }


    }
}
