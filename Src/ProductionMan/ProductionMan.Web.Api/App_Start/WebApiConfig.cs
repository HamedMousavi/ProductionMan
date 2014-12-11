using ProductionMan.Web.Api.Services;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace ProductionMan.Web.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.EnableSystemDiagnosticsTracing();

            config.Services.Add(typeof(IExceptionLogger), new DefaultExceptionLogger());
        }
    }
}
