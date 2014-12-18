using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ProductionMan.Web.Api.Common.Models;


namespace ProductionMan.Domain.WebServices
{

    public class Membership : WebServiceBase
    {

        private const string PermissionsUrl = "Permissions";
        
        
        public async Task<List<Permission>> GetPermissions()
        {
            List<Permission> permissions = null;

            using (var client = new HttpClient())
            {
                PrepareHeaders(client, true);

                // HTTP GET
                var response = await client.GetAsync(PermissionsUrl);
                if (response.IsSuccessStatusCode)
                {
                    permissions = response.Content.ReadAsAsync<List<Permission>>().Result;
                }
            }

            return permissions;
        }
    }
}
