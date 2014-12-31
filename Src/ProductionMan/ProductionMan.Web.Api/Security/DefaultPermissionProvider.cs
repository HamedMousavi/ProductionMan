using ProductionMan.Data.Shared.Models;
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
                var permissions = DataProxy.Instance.
                    MembershipRepository.GetPermissions(user);

                _claims = new ClaimList();
                _claims.AddRange(permissions.Select(CreateClaim));

                return Task.FromResult(_claims);
            }
            return Task.FromResult<ClaimList>(null);
        }

        public Task<ClaimList> GetCachedClaims()
        {
            return Task.FromResult(_claims);
        }


        private Claim CreateClaim(Permission permission)
        {
            // get:/api/users
            var url = string.Format("{0}:{1}",
                DataProxy.Instance.GetMethodByPermission(permission.Operation),
                permission.ResourceName);

            return new Claim(ClaimTypes.Uri, url);
        }
    }
}