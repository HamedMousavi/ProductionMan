using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;


namespace ProductionMan.Web.Api.Security
{
    
    
    using System.Web.Http.Filters;

    
    public class DefaultIAuthorizer : IAuthorizationFilter
    {
    
        public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, System.Func<System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage>> continuation)
        {
            //System.Web.Http.AuthorizeAttribute

            return Task.FromResult<HttpResponseMessage>(null);
        }


        public bool AllowMultiple
        {
            get { return false; }
        }
    }
}