using ProductionMan.Data.Shared.Models;
using ProductionMan.Web.Api.Logic;
using ProductionMan.Web.Api.Security.Models;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;


namespace ProductionMan.Web.Api.Security
{

    public class DefaultAuthenticator : BaseAuthenticator
    {

        protected override async Task<IPrincipal> AuthenticateAsync(Credentials credentials,
            CancellationToken cancellationToken)
        {
            var claims = await LoadPermissions(credentials, cancellationToken);
            return claims == null ? null : new DefaultPrincipal(claims);
        }


        private static Task<ClaimList> LoadPermissions(Credentials credentials, CancellationToken cancellationToken)
        {
            var data = DataProxy.Instance.MembershipRepository;
            var user = data.Find(credentials.Username, credentials.Password);

            if (user != null)
            {
                var permissions = DataProxy.Instance.
                    MembershipRepository.GetPermissions(user);

                var claims = new ClaimList();
                claims.AddRange(permissions.Select(CreateClaim));

                return Task.FromResult(claims);
            }

            return Task.FromResult<ClaimList>(null);
        }


        private static Claim CreateClaim(Permission permission)
        {
            // get:/api/users
            var url = string.Format("{0}:{1}", 
                DataProxy.Instance.GetMethodByPermission(permission.Operation), 
                permission.ResourceName);

            return new Claim(ClaimTypes.Uri, url);
        }
    }
}