using System.Collections.Generic;
using System.Net.Http;


namespace ProductionMan.Web.Api.Security.Content
{

    public class DefaultContentSecurityPoliciesController : IContentSecurityPoliciesController
    {
        
        private List<IContentSecurityPolicy> _contentSecurityEnforcers;


        public void AddContentSecurityPolicy(IContentSecurityPolicy policy)
        {
            if (_contentSecurityEnforcers == null)
            {
                _contentSecurityEnforcers = new List<IContentSecurityPolicy>();
            }
            _contentSecurityEnforcers.Add(policy);
        }


        public void EnforceContentSecurityPolicy(HttpResponseMessage response, ObjectContent content)
        {
            if (_contentSecurityEnforcers != null)
            {
                foreach (var policy in _contentSecurityEnforcers)
                {
                    if (policy.CanHandleResponse(response))
                    {
                        policy.ApplySecurityToResponseData(content);
                    }
                }
            }
        }
    }
}