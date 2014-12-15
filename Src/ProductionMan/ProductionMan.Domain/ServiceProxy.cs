using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace ProductionMan.Domain
{

    public class ServiceProxy
    {

        public static string Username { get; set; }
        public static string Password { get; set; }


        private const string PermissionsUrl = "Security/Permissions";
        private static readonly Uri BaseAddress = new Uri("http://localhost/ProductionMan/api/");
        private static readonly MediaTypeWithQualityHeaderValue MediaType = new MediaTypeWithQualityHeaderValue("application/json");
        private static AuthenticationHeaderValue _credentials;


        static ServiceProxy()
        {
            
        }


        public static void SetCredentials(string username, string password)
        {
            _credentials = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", username, password))));
        }


        public static async Task<List<string>> GetPermissions()
        {
            List<string> permissions = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(MediaType);
                client.DefaultRequestHeaders.Authorization = _credentials;

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
