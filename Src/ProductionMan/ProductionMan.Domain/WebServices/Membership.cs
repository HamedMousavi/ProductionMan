using System.Net;
using ProductionMan.Web.Api.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ProductionMan.Domain.WebServices
{

    public class Membership : WebServiceBase
    {
        private UserRead _user;

        private const string UsersUrl = "Users";
        private const string ProfilesUrl = "Profiles";
        private const string RolesUrl = "Roles";
        private const string PermissionsUrl = "Permissions";


        public async Task<ServiceCallResult<UserRead>> GetUserProfile()
        {
            var response = await RequestGet<UserRead>(ProfilesUrl);
            if (response != null && response.CallStatusCode == HttpStatusCode.OK)
            {
                _user = response.Results;
            }

            return response;
        }


        public async Task<ServiceCallResult<List<Permission>>> GetPermissions()
        {
            if (_user == null) return null;
            return await RequestGet<List<Permission>>(
                string.Format("{0}/{1}/{2}", UsersUrl, _user.UserId, PermissionsUrl));
        }


        public async Task<ServiceCallResult<List<UserRead>>> GetUsers()
        {
            return await RequestGet<List<UserRead>>(UsersUrl);
        }


        public async Task<ServiceCallResult<List<UserRole>>> GetRoles()
        {
            return await RequestGet<List<UserRole>>(RolesUrl);
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

        public async Task<bool> CreateRole(UserRole role)
        {
            var response = await RequestCreate(RolesUrl, role);
            if (response != null)
            {
                role.RoleId = response.Results.RoleId;
            }

            return response != null;
        }

        public async Task<bool> DeleteRole(UserRole role)
        {
            return await RequestDelete(RolesUrl + string.Format("/{0}", role.RoleId));
        }

        public async Task<bool> Update(UserRole role)
        {
            return await RequestUpdate(RolesUrl, role);
        }
    }
}
