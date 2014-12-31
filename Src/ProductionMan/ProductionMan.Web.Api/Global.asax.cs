using System.Web.Routing;
using AutoMapper;
using log4net;
using log4net.Config;
using ProductionMan.Data.Shared.Models;
using ProductionMan.Web.Api.Common.Models;
using System.Configuration;
using System.Web.Http;


namespace ProductionMan.Web.Api
{

    public class WebApiApplication : System.Web.HttpApplication
    {

        private ILog _logger;


        protected void Application_Start()
        {
            // Configure logger
            ConfigureLogger();

            // Configure app
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // Configure database
            ConfigureDatabase();

            // Auto Mapper object mapping
            ConfigureAutoMapper();
        }


        private void ConfigureLogger()
        {
            XmlConfigurator.Configure();
            Log.Info("Application Started!");
        }


        private void ConfigureDatabase()
        {
            Data.Settings.ConnectionString =
                ConfigurationManager.ConnectionStrings["ProductionMan.Web.Api.Properties.Settings.ProductionMan"].ToString();
        }


        // Errors that occur before error handler is installed in configuration
        protected void Application_Error()
        {
            var exception = Server.GetLastError();
            if (exception != null)
            {
                Log.Error("Unhandled exception.", exception);
            }
        }


        private static void ConfigureAutoMapper()
        {
            Mapper.CreateMap<Common.Models.Permission, Data.Shared.Models.Permission>();
            Mapper.CreateMap<Data.Shared.Models.Permission, Common.Models.Permission>();

            Mapper.CreateMap<Role, UserRole>();
            Mapper.CreateMap<UserRole, Role>();

            Mapper.CreateMap<User, UserRead>();
            Mapper.CreateMap<User, UserWrite>();
            Mapper.CreateMap<UserWrite, User>();
        }


        private ILog Log
        {
            get 
            {
                return _logger ??
                       (_logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType));
            }
        }
    }
}
