using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Fotm.DAL.Util
{
    /// <summary>
    /// Class to read from the global Personal.config file.
    /// </summary>
    public static class ConfigUtil
    {
        public static string API_Key, SQL_Server;

        // xml keys
        private static readonly string API_XML = "API_Key";
        private static readonly string SQL_XML = "SQL_Server";
        private static readonly string CONFIG_PATH = "..\\..\\..\\Personal.config";

        static ConfigUtil()
        {
            XDocument xdoc = XDocument.Load(CONFIG_PATH);
            API_Key = xdoc.Descendants(API_XML).First().Value;
            SQL_Server = xdoc.Descendants(SQL_XML).First().Value;
        }

        public static void SetConfig(string apiKey, string sqlServer)
        {
            var xdoc = XDocument.Load(CONFIG_PATH);
            xdoc.Descendants(API_XML).First().SetValue(apiKey);
            xdoc.Descendants(SQL_XML).First().SetValue(sqlServer);
            using (var writer = new XmlTextWriter(CONFIG_PATH, Encoding.UTF8))
            {
                xdoc.WriteTo(writer);
            }

            API_Key = apiKey;
            SQL_Server = sqlServer;
        }
    }
}
