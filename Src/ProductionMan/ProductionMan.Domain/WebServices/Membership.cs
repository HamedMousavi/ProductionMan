using ProductionMan.Web.Api.Common.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;


namespace ProductionMan.Domain.WebServices
{

    public class Membership : WebServiceBase
    {

        private const string UserListUrl = "Users";
        private const string UserCreateUrl = "Users";
        private const string RoleListUrl = "Roles";
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


        public async Task<bool> CreateUser(UserWrite user)
        {
            var result = new ServiceCallResult<List<UserRead>>();

            using (var client = new HttpClient())
            {
                PrepareHeaders(client);

                var response = await client.PostAsJsonAsync(UserCreateUrl, user);
                result.CallStatusCode = response.StatusCode;
                result.CallStatusMessage = response.ReasonPhrase;
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }

            return false;
        }


        public void UpdateUser(UserWrite user)
        {
            throw new System.NotImplementedException();
        }


        public void DeleteUser(UserRead user)
        {
            throw new System.NotImplementedException();
        }


        public async Task<ServiceCallResult<List<UserRole>>> GetRoles()
        {
            var result = new ServiceCallResult<List<UserRole>>();

            using (var client = new HttpClient())
            {
                PrepareHeaders(client);

                var response = await client.GetAsync(RoleListUrl);
                result.CallStatusCode = response.StatusCode;
                result.CallStatusMessage = response.ReasonPhrase;
                if (response.IsSuccessStatusCode)
                {
                    result.Results = response.Content.ReadAsAsync<List<UserRole>>().Result;
                }
            }

            return result;
        }
    }
}
