using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace ProductionMan.Domain.WebServices
{

    public class Membership : WebServiceBase
    {

        private const string PermissionsUrl = "Users";
        
        
        public async Task<List<string>> GetPermissions()
        {
            List<string> permissions = null;

            using (var client = new HttpClient())
            {
                PrepareHeaders(client, true);

                // HTTP GET
                var response = await client.GetAsync(PermissionsUrl);
                if (response.IsSuccessStatusCode)
                {
                    permissions = response.Content.ReadAsAsync<List<string>>().Result;
                }
            }

            return permissions;
        }
    }
}
