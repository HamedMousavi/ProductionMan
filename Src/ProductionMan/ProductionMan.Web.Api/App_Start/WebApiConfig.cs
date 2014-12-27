using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Tracing;
using AutoMapper;
using ProductionMan.Data.Shared.Models;
using ProductionMan.Web.Api.Common.Models;
using ProductionMan.Web.Api.Security;
using ProductionMan.Web.Api.Services;

namespace ProductionMan.Web.Api
{

    public static class WebApiConfig
    {

        public static void Register(HttpConfiguration config)
        {
            // Tracing, logging, and error handling
            ConfigureAppServices(config);

            // Authentication & Authorization
            ConfigureSecurity(config);

            ConfigureAutoMapper();

            // Web API routes
            ConfigureRoutes(config);
        }


        private static void ConfigureRoutes(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional }
            );
        }


        private static void ConfigureSecurity(HttpConfiguration config)
        {
            // Disable IIS or OWIN authentication modules/filters, 
            //  we want to authenticate in WebApi regardless of previous authenticators.
            //  This call actually UnAuthenticates requests first! (removes previous IPrincipals)
            config.SuppressHostPrincipal();

            // Authentication
            config.Filters.Add(new DefaultAuthenticator());

            // Authorization
            //config.Filters.Add(new DefaultIAuthorizer());
        }


        private static void ConfigureAppServices(HttpConfiguration config)
        {
            // Tracing
            //config.EnableSystemDiagnosticsTracing();
            config.Services.Replace(typeof(ITraceWriter), new Log4NetTraceWriter());

            // Error logging
            config.Services.Add(typeof(IExceptionLogger), new DefaultExceptionLogger());
        }


        private static void ConfigureAutoMapper()
        {
            Mapper.CreateMap<Role, UserRole>();
            Mapper.CreateMap<UserRole, Role>();

            Mapper.CreateMap<User, UserRead>();
            Mapper.CreateMap<User, UserWrite>();
            Mapper.CreateMap<UserWrite, User>();
        }
    }
}
