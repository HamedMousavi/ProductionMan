using System;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Controllers;
using log4net;
using log4net.Repository.Hierarchy;


namespace ProductionMan.Web.Api.Security
{

    public class DefaultIAuthorizer : AuthorizeAttribute
    {
        
        private ILog _logger;


        protected override bool IsAuthorized(HttpActionContext context)
        {
            Log.Info(">> In IsAuthorized");
            var principal = Thread.CurrentPrincipal as DefaultPrincipal;

            if (principal != null && context != null && context.Request != null && context.Request.RequestUri != null)
            {
                var url = context.Request.RequestUri.AbsolutePath;
                var permission = 
                    string.Format("{0}:{1}", 
                    context.Request.Method.Method.ToLower(),
                    context.Request.RequestUri.AbsoluteUri.Substring(
                    context.Request.RequestUri.AbsoluteUri.IndexOf("/api/", StringComparison.Ordinal))).ToLower();

                Log.Info(string.Format(">> Checking claim Uri={0}, permission={1}", url, permission));

                foreach (var claim in principal.Claims)
                {
                    if (string.Equals(claim.Value, permission, StringComparison.InvariantCultureIgnoreCase))
                    {
                        Log.Info(string.Format(">> Claim FOUND! {0}", claim.Value));
                        return true;
                    }
                }
                Log.Info(string.Format(">> Claim not found"));
            }

            return false;
        }


        // If 401 – Unauthorized is okay for you, no need to override
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            base.HandleUnauthorizedRequest(actionContext);
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