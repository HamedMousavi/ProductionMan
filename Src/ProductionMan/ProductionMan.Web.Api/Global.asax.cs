using log4net;
using log4net.Config;
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
        }


        private void ConfigureLogger()
        {
            XmlConfigurator.Configure();
            Log.Info("Testing logger!");
        }


        protected void Application_Error()
        {
            var exception = Server.GetLastError();
            if (exception != null)
            {
                Log.Error("Unhandled exception.", exception);
            }
        }


        public ILog Log
        {
            get 
            {
                return _logger ??
                       (_logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType));
            }
        }
    }
}
