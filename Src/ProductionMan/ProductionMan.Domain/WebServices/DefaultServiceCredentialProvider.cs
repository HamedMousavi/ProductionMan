using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace ProductionMan.Domain.WebServices
{

    public class DefaultServiceCredentialProvider : IServiceCredentialProvider
    {

        private AuthenticationHeaderValue _serviceCredentials;

        
        public void SetCredentials(string username, string password)
        {
            _serviceCredentials = new AuthenticationHeaderValue("Basic", 
                Convert.ToBase64String(Encoding.ASCII.GetBytes(
                    string.Format("{0}:{1}", username, password))));
        }


        public void AttachCredentials(HttpClient client)
        {
            if (_serviceCredentials != null)
                client.DefaultRequestHeaders.Authorization = _serviceCredentials;
        }
    }
}
