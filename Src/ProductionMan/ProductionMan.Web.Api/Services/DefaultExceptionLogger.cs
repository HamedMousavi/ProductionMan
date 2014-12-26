using System.Web.Http.ExceptionHandling;
using log4net;

namespace ProductionMan.Web.Api.Services
{

    public class DefaultExceptionLogger : ExceptionLogger
    {

         private readonly ILog _log;


         public DefaultExceptionLogger()
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }


        public override void Log(ExceptionLoggerContext context)
        {
            _log.Error("Unhandled exception", context.Exception);
        }
   }
}