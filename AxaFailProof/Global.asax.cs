using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AxaFailProof
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "FailProofYourHealth", // Route name
                "Fail-Proof-Your-Health", // URL with parameters
                new { controller = "Home", action = "FailProofYourHealth", id = 7 } // Parameter defaults
            );

            routes.MapRoute(
                "HealthRiskScore", // Route name
                "Health-Risk-Score", // URL with parameters
                new { controller = "Home", action = "HealthRiskScore"} // Parameter defaults
            );

            routes.MapRoute(
                "HealthFactsFigures", // Route name
                "Health-Facts-Figures", // URL with parameters
                new { controller = "Home", action = "HealthFactsFigures", id = 11 } // Parameter defaults
            );

            routes.MapRoute(
                "HealthFactsFiguresDetails", // Route name
                "Health-Facts-Figures/{id}/{title}", // URL with parameters
                new { controller = "Home", action = "HealthDetail", id = UrlParameter.Optional, title = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "HealthMax", // Route name
                "Health-Max", // URL with parameters
                new { controller = "Home", action = "FailProofYourHealth", id = 9 } // Parameter defaults
            );

            routes.MapRoute(
                "HealthExentials", // Route name
                "Health-Exentials", // URL with parameters
                new { controller = "Home", action = "FailProofYourHealth", id = 8 } // Parameter defaults
            );

            routes.MapRoute(
                "WhatIsFailProof", // Route name
                "what-is-fail-proof", // URL with parameters
                new { controller = "Home", action = "WhatIsFailProof", id = 3 } // Parameter defaults
            );

            routes.MapRoute(
                "StoryDetails", // Route name
                "Story/{id}/{title}", // URL with parameters
                new { controller = "Home", action = "Story", id = UrlParameter.Optional, title = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "Default", // Route name
                "{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "SEO", // Route name
                "{seo}/{action}", // URL with parameters
                new { controller = "Home", action = "Index", seo = UrlParameter.Optional} // Parameter defaults
            );

            routes.MapRoute(
                "ArchiveByDate", // Route name
                "{action}/{month}/{year}", // URL with parameters
                new { controller = "Home", action = "ArchiveByDate", month = UrlParameter.Optional, year = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "NotFound", // Route name
                "NotFound", // URL with parameters
                new { controller = "Home", action = "NotFound" } // Parameter defaults
            );



        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}