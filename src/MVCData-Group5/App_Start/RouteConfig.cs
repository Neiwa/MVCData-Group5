using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCData_Group5
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "MovieIndexPage1",
                url: "Movie/",
                defaults: new { controller = "Movie", action = "Index" }
            );
            routes.MapRoute(
                name: "MovieIndex",
                url: "Movie/Page/{page}",
                defaults: new { controller = "Movie", action = "List" },
                constraints: new { page = @"\d+" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
