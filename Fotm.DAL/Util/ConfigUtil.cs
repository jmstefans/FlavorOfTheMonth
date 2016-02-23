using System.Linq;
using System.Xml.Linq;

namespace Fotm.DAL.Util
{
    /// <summary>
    /// Class to read from the global Personal.config file.
    /// </summary>
    public static class ConfigUtil
    {
        public static string API_Key, SQL_Server;

        static ConfigUtil()
        {
            XDocument xdoc = XDocument.Load("..\\..\\..\\Personal.config");
            API_Key = xdoc.Descendants("API_Key").First().Value;
            SQL_Server = xdoc.Descendants("SQL_Server").First().Value;
        }
    }
}
