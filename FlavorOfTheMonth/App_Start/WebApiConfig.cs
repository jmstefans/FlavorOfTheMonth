using System.Web.Http;

namespace FlavorOfTheMonth
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // make all web-api requests to be sent over https
            // WARNING: This approach couples WebApi to System.Web libraries 
            //          and you won’t be able to use this code in self-hosed
            //          WebApi applications. If this is a problem then check
            //          out http://www.strathweb.com/2013/01/adding-request-islocal-to-asp-net-web-api/
            config.MessageHandlers.Add(new EnforceHttpsHandler());
        }
    }
}
