using log4net;
using log4net.Config;
using System.Web.Http;


namespace ProductionMan.Web.Api
{

    public class WebApiApplication : System.Web.HttpApplication
    {

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
            var logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            logger.Info("Testing logger!");
        }
    }
}
