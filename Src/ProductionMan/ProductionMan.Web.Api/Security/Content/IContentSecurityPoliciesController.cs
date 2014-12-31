using System.Net.Http;

namespace ProductionMan.Web.Api.Security.Content
{
    public interface IContentSecurityPoliciesController
    {
        void EnforceContentSecurityPolicy(HttpResponseMessage response, ObjectContent content);
    }
}