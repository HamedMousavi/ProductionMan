using System;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Controllers;


namespace ProductionMan.Web.Api.Security
{

    public class DefaultIAuthorizer : AuthorizeAttribute
    {

        protected override bool IsAuthorized(HttpActionContext context)
        {
            var principal = Thread.CurrentPrincipal as DefaultPrincipal;

            if (principal != null && context != null && context.Request != null && context.Request.RequestUri != null)
            {
                var url = "/api/" + context.ControllerContext.ControllerDescriptor.ControllerName;
                var permission = string.Format("{0}:{1}", context.Request.Method.Method.ToLower(), url);

                foreach (var claim in principal.Claims)
                {
                    if (string.Equals(claim.Value, permission, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        // If 401 – Unauthorized is okay for you, no need to override
        //protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        //{
        //    base.HandleUnauthorizedRequest(actionContext);
        //}
    }
}