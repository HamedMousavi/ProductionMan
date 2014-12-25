using ProductionMan.Web.Api.Common.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;


namespace ProductionMan.Web.Api.ActionResults
{

    public class CreatedActionResult<T> : IHttpActionResult where T : ILinkable
    {

        private readonly T _createdItem;
        private readonly HttpRequestMessage _requestMessage;


        public CreatedActionResult(HttpRequestMessage requestMessage, T createdItem)
        {
            _requestMessage = requestMessage;
            _createdItem = createdItem;
        }


        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }


        public HttpResponseMessage Execute()
        {
            var acceptHeader = _requestMessage.Headers.Accept.FirstOrDefault();
            var mediaType = acceptHeader == null ? null : acceptHeader.MediaType;

            var responseMessage = string.IsNullOrWhiteSpace(mediaType)
                ? _requestMessage.CreateResponse(HttpStatusCode.Created, _createdItem)
                : _requestMessage.CreateResponse(HttpStatusCode.Created, _createdItem, mediaType);

            responseMessage.Headers.Location = LocationLinkCalculator.GetLocationLink(_createdItem);

            return responseMessage;
        }
    }
}