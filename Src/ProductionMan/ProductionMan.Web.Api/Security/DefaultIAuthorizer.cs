using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ProductionMan.Web.Api.Security
{
    public class DefaultIAuthorizer : IAuthorizationFilter
    {
    
        public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, System.Func<Task<HttpResponseMessage>> continuation)
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