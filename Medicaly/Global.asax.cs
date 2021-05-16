using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Medicaly
{
    public class MvcApplication : System.Web.HttpApplication
    {

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Konsultasi",
                url: " Doctor/Konsultasi/{id}",
                defaults: new { controller = "Doctor", action = "Konsultasi" }
            );

            routes.MapRoute(
                name: "Product",
                url: " Product/Detail/{id}",
                defaults: new { controller = "Product", action = "Detail" }
            );

            routes.MapRoute(
                name: "Pharmacy",
                url: " Pharmacy/Products/Manages",
                defaults: new { controller = "Pharmacy", action = "Products", id = "" }
            );

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
