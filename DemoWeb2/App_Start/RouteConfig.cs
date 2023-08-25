﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DemoWeb2
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "N_Shopping",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "CustomerProduct", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
