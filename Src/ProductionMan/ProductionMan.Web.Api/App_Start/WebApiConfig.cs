﻿using ProductionMan.Web.Api.Security;
using ProductionMan.Web.Api.Security.Content;
using ProductionMan.Web.Api.Services;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Tracing;


namespace ProductionMan.Web.Api
{

    public static class WebApiConfig
    {

        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            ConfigureRoutes(config);

            // Tracing, logging, and error handling
            ConfigureAppServices(config);

            // Authentication & Authorization
            ConfigureSecurity(config);

            //// http://stackoverflow.com/questions/12976352/asp-net-web-api-model-binding-not-working-with-xml-data-on-post
            config.Formatters.XmlFormatter.UseXmlSerializer = true;
        }


        private static void ConfigureRoutes(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    "DefaultApi", "api/{controller}/{id}",
            //    new {id = RouteParameter.Optional});
        }


        private static void ConfigureSecurity(HttpConfiguration config)
        {
            // Disable IIS or OWIN authentication modules/filters, 
            //  we want to authenticate in WebApi regardless of previous authenticators.
            //  This call actually UnAuthenticates requests first! (removes previous IPrincipals)
            config.SuppressHostPrincipal();

            // Register a content policy enforcer handler
            var permissionProvider = new DefaultPermissionProvider();
            var policyMan = new DefaultContentSecurityPoliciesController();
            policyMan.AddContentSecurityPolicy(new RolePermissionsPolicy(permissionProvider));
            config.MessageHandlers.Add(new DefaultContentControlMessageHandler(policyMan));

            // Authentication
            config.Filters.Add(new DefaultAuthenticator(permissionProvider));

            // Authorization
            config.Filters.Add(new DefaultIAuthorizer());
        }


        private static void ConfigureAppServices(HttpConfiguration config)
        {
            // Tracing
            //config.EnableSystemDiagnosticsTracing();
            config.Services.Replace(typeof(ITraceWriter), new Log4NetTraceWriter());

            // Error logging
            config.Services.Add(typeof(IExceptionLogger), new DefaultExceptionLogger());

            // Error handling
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
        }
    }
}
