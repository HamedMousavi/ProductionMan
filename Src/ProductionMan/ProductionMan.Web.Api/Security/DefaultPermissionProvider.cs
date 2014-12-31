using System;
using System.Collections.Generic;
using AutoMapper;
using ProductionMan.Web.Api.Common.Models;
using ProductionMan.Web.Api.Logic;
using ProductionMan.Web.Api.Security.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;


namespace ProductionMan.Web.Api.Security
{

    public class DefaultPermissionProvider : IPermissionProvider
    {
        private ClaimList _claims;

        public Task<ClaimList> GetClaims(Credentials credentials, CancellationToken cancellationToken)
        {
            if (credentials == null) return Task.FromResult<ClaimList>(null);
            if (_claims != null)
            {
                _claims.Clear();
                _claims = null;
            }

            var data = DataProxy.Instance.MembershipRepository;
            var user = data.FindUser(credentials.Username, credentials.Password);

            if (user != null)
            {
                var items = DataProxy.Instance.
                    MembershipRepository.GetPermissionsByRoleId(user.Role.RoleId);

                var permissions = new List<Common.Models.Permission>();
                permissions.AddRange(items.Select(Mapper.Map<Common.Models.Permission>));
                Permissions = permissions;
                User = Mapper.Map<UserRead>(user);

                _claims = new ClaimList();
                _claims.AddRange(Permissions.Select(CreateClaim));

                return Task.FromResult(_claims);
            }
            return Task.FromResult<ClaimList>(null);
        }

        public Task<ClaimList> GetCachedClaims()
        {
            return Task.FromResult(_claims);
        }

        public IEnumerable<Permission> Permissions { get; private set; }
        
        
        public UserRead User { get; set; }


        private Claim CreateClaim(Common.Models.Permission permission)
        {
            // get:/api/users
            var url = string.Format("{0}:{1}",
                DataProxy.Instance.GetMethodByPermission((Data.Shared.Models.Permission.OperationType)(Int32)permission.Operation),
                permission.ResourceName);

            return new Claim(ClaimTypes.Uri, url);
        }
    }
}