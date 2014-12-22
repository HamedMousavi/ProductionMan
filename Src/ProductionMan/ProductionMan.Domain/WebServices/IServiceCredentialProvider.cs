using System.Net.Http;

namespace ProductionMan.Domain.WebServices
{
    public interface IServiceCredentialProvider
    {
        void SetCredentials(string username, string password);
        void AttachCredentials(HttpClient client);
    }
}
