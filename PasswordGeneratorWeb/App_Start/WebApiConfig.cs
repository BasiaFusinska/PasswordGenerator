﻿using System.Web.Http;
using System.Web.Routing;

namespace PasswordGeneratorWeb
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Generator",
                routeTemplate: "api/generate",
                defaults: new { controller = "Password", action = "Generate" },
                constraints: new { httpMethod = new HttpMethodConstraint("POST") }
            );

            config.Routes.MapHttpRoute(
                name: "Veryfier",
                routeTemplate: "api/verify",
                defaults: new { controller = "Password", action = "Verify" }
            );

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
        }
    }
}
