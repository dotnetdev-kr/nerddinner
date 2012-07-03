using System.Web.Mvc;
using System.Web.Routing;

namespace NerdDinner
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                    "PrettyDetails",
                    "{Id}",
                        new { controller = "Dinners", action = "Details" },
                        new { Id = @"\d+" }
                    );

            routes.MapRoute(
                    "UpcomingDinners",
                    "Dinners/Page/{page}",
                    new { controller = "Dinners", action = "Index" }
            );

            routes.MapRoute(
                    "Default",                                              // Route name
                    "{controller}/{action}/{id}",                           // URL with parameters
                    new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
            );

            routes.MapRoute(
                "OpenIdDiscover",
                "Auth/Discover");

        }
    }
}