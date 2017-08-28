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
                name: "MovieListPage1",
                url: "Movie/Page/1",
                defaults: new { controller = "Movie", action = "List", page = 1 }
            );
            routes.MapRoute(
                name: "MovieList",
                url: "Movie/Page/{page}",
                defaults: new { controller = "Movie", action = "List" },
                constraints: new { page = @"\d+" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Movie", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
