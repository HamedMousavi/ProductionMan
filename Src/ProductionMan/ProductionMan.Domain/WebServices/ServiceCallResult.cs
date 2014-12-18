using System.Net;

namespace ProductionMan.Domain.WebServices
{
    public class ServiceCallResult<T>
    {
        public T Results { get; set; }
        public string CallStatusMessage { get; set; }
        public HttpStatusCode CallStatusCode { get; set; }
    }
}
