using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ProductionMan.Web.Api.Common.Models;


namespace ProductionMan.Domain.WebServices
{

    public class Membership : WebServiceBase
    {

        private const string UserListUrl = "Users";
        private const string CurrentUserUrl = "Users/Current";
        private const string PermissionsUrl = "Permissions";


        public async Task<ServiceCallResult<List<Permission>>> GetPermissions()
        {
            var result = new ServiceCallResult<List<Permission>>();

            using (var client = new HttpClient())
            {
                PrepareHeaders(client);

                var response = await client.GetAsync(PermissionsUrl);
                result.CallStatusCode = response.StatusCode;
                result.CallStatusMessage = response.ReasonPhrase;
                if (response.IsSuccessStatusCode)
                {
                    result.Results = response.Content.ReadAsAsync<List<Permission>>().Result;
                }
            }

            return result;
        }


        public async Task<ServiceCallResult<UserRead>> GetUserDetails()
        {
            var result = new ServiceCallResult<UserRead>();
            using (var client = new HttpClient())
            {
                PrepareHeaders(client);

                var response = await client.GetAsync(CurrentUserUrl);
                result.CallStatusCode = response.StatusCode;
                result.CallStatusMessage = response.ReasonPhrase;
                if (response.IsSuccessStatusCode)
                {
                    result.Results = response.Content.ReadAsAsync<UserRead>().Result;
                }
            }

            return result;
        }


        public async Task<ServiceCallResult<List<UserRead>>> GetUsers()
        {
            var result = new ServiceCallResult<List<UserRead>>();

            using (var client = new HttpClient())
            {
                PrepareHeaders(client);

                var response = await client.GetAsync(UserListUrl);
                result.CallStatusCode = response.StatusCode;
                result.CallStatusMessage = response.ReasonPhrase;
                if (response.IsSuccessStatusCode)
                {
                    result.Results = response.Content.ReadAsAsync<List<UserRead>>().Result;
                }
            }

            return result;
        }


        public void CreateUser(UserRead user)
        {
            throw new System.NotImplementedException();
        }


        public void UpdateUser(UserRead user)
        {
            throw new System.NotImplementedException();
        }


        public void DeleteUser(UserRead user)
        {
            throw new System.NotImplementedException();
        }
    }
}
