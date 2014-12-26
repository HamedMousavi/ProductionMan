using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace ProductionMan.Web.Api.Security
{

    // Modified version of MS Basic Authentication sample
    public class AuthenticationFailureResult : IHttpActionResult
    {

        private readonly HttpRequestMessage _request;
        private readonly string _reasonPhrase;


        public AuthenticationFailureResult(string reasonPhrase, HttpRequestMessage request)
        {
            _reasonPhrase = reasonPhrase;
            _request = request;
        }
        

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }


        private HttpResponseMessage Execute()
        {
            return new HttpResponseMessage(HttpStatusCode.Unauthorized)
            {
                RequestMessage = _request,
                ReasonPhrase = _reasonPhrase
            };
        }
    }
}