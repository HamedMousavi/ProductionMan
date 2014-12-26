using System.Configuration;
using System.Web.Http;
using log4net;
using log4net.Config;

namespace ProductionMan.Web.Api
{

    public class WebApiApplication : System.Web.HttpApplication
    {

        private ILog _logger;


        protected void Application_Start()
        {
            // Configure logger
            ConfigureLogger();

            // Configure database
            ConfigureDatabase();

            // Configure app
            GlobalConfiguration.Configure(WebApiConfig.Register);
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


        protected void Application_Error()
        {
            var exception = Server.GetLastError();
            if (exception != null)
            {
                Log.Error("Unhandled exception.", exception);
            }
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
