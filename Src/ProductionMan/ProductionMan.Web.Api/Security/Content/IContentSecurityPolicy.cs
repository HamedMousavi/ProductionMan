using System.Net.Http;
using System.Threading.Tasks;


namespace ProductionMan.Web.Api.Security.Content
{
    public interface IContentSecurityPolicy
    {
        bool CanHandleResponse(HttpResponseMessage response);

        Task ApplySecurityToResponseData(ObjectContent responseObjectContent);
    }
}