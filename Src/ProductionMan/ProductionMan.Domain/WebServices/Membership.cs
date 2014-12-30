using ProductionMan.Web.Api.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ProductionMan.Domain.WebServices
{

    public class Membership : WebServiceBase
    {

        private const string UsersUrl = "Users";
        private const string CurrentUserUrl = "Users/Current";
        private const string RoleListUrl = "Roles";
        private const string PermissionsUrl = "Permissions";


        public async Task<ServiceCallResult<List<Permission>>> GetPermissions()
        {
            return await RequestGet<List<Permission>>(PermissionsUrl);
        }


        public async Task<ServiceCallResult<UserRead>> GetUserDetails()
        {
            return await RequestGet<UserRead>(CurrentUserUrl);
        }


        public async Task<ServiceCallResult<List<UserRead>>> GetUsers()
        {
            return await RequestGet<List<UserRead>>(UsersUrl);
        }


        public async Task<ServiceCallResult<List<UserRole>>> GetRoles()
        {
            return await RequestGet<List<UserRole>>(RoleListUrl);
        }


        public async Task<bool> CreateUser(UserWrite user)
        {
            var response = await RequestCreate<UserRead, UserWrite>(UsersUrl, user);
            if (response != null)
            {
                user.UserId = response.Results.UserId;
            }

            return response != null;
        }


        public async Task<bool> DeleteUser(UserRead user)
        {
            return await RequestDelete(UsersUrl + string.Format("/{0}", user.UserId));
        }


        public async Task<bool> UpdateUser(UserWrite user)
        {
            return await RequestUpdate(UsersUrl, user);
        }
    }
}
