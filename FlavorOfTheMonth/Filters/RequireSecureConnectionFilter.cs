using System;
using System.Web.Mvc;

namespace FlavorOfTheMonth.Filters
{
    /// <summary>
    /// Class to filter all requests through which will require https except when request is local (development machine).
    /// </summary>
    public class RequireSecureConnectionFilter : RequireHttpsAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (filterContext.HttpContext.Request.IsLocal)
            {
                // when connection to the application is local, don't do any HTTPS stuff
                return;
            }

            base.OnAuthorization(filterContext);
        }
    }
}