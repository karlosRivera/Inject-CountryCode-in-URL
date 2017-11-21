using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace InjectCountryCodeInURL
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //var settings = new FriendlyUrlSettings();
            //settings.AutoRedirectMode = RedirectMode.Permanent;
            //routes.EnableFriendlyUrls(settings);

            //routes.MapPageRoute(
            //"root-with-countrycode",
            //"{countrycode}/Default",
            //"~/Default.aspx");

            //routes.MapPageRoute(
            //"default-with-countrycode",
            //"{countrycode}",
            //"~/Default.aspx");
        }
    }
}
