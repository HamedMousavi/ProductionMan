using ProductionMan.Web.Api.Common.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;


namespace ProductionMan.Web.Api.Security.Content
{

    public class RolePermissionsPolicy : IContentSecurityPolicy
    {

        private readonly IPermissionProvider _permissionProvider;


        public RolePermissionsPolicy(IPermissionProvider permissionProvider)
        {
            _permissionProvider = permissionProvider;
        }


        public bool CanHandleResponse(HttpResponseMessage response)
        {
            var objectContent = response.Content as ObjectContent;
            var policyApplies = objectContent != null && 
                (objectContent.ObjectType == typeof(UserRole) ||
                 objectContent.ObjectType == typeof(IEnumerable<UserRole>));

            return policyApplies;
        }


        public async Task ApplySecurityToResponseData(ObjectContent responseObjectContent)
        {
            var role = responseObjectContent.Value as UserRole;
            var roles = responseObjectContent.Value as IEnumerable<UserRole>;

            if (roles != null)
            {
                foreach (var item in roles)
                {
                    item.SetShouldSerializePermissions(await PermissionsClaimIsGranted());
                }
            }

            if (role != null)
            {
                role.SetShouldSerializePermissions(await PermissionsClaimIsGranted());
            }
        }


        private async Task<bool> PermissionsClaimIsGranted()
        {
            if (_permissionProvider != null)
            {
                var claims = await _permissionProvider.GetCachedClaims();
                if (claims != null)
                {
                    foreach (var claim in claims)
                    {
                        if (string.Equals(claim.Value, "get:/api/roles/permissions",
                            StringComparison.InvariantCultureIgnoreCase))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}