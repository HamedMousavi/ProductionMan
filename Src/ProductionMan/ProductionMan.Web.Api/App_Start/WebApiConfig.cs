using System.Web.Http;
using log4net;
using log4net.Config;

namespace ProductionMan.Web.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            var logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.EnableSystemDiagnosticsTracing();

            XmlConfigurator.Configure();
            logger.Info("Testing logger!");
        }
    }
}
