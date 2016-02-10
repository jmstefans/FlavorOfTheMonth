using FlavorOfTheMonth.Filters;
using System.Web.Mvc;

namespace FlavorOfTheMonth
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new RequireSecureConnectionFilter());
            filters.Add(new HandleErrorAttribute());
        }
    }
}