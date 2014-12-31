using ProductionMan.Web.Api.Security.Content;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;


namespace ProductionMan.Web.Api.Security
{

    public class DefaultContentControlMessageHandler : DelegatingHandler
    {

        private readonly IContentSecurityPoliciesController _contentController;


        public DefaultContentControlMessageHandler(IContentSecurityPoliciesController contentController)
        {
            _contentController = contentController;
        }


        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            _contentController.EnforceContentSecurityPolicy(response, (ObjectContent)response.Content);

            return response;
        }
    }
}