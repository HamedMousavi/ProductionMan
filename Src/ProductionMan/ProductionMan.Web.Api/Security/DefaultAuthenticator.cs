using ProductionMan.Web.Api.Security.Models;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;


namespace ProductionMan.Web.Api.Security
{

    public class DefaultAuthenticator : BaseAuthenticator
    {

        private readonly IPermissionProvider _permissionProvider;


        public DefaultAuthenticator(IPermissionProvider permissionProvider)
        {
            _permissionProvider = permissionProvider;
        }


        protected override async Task<IPrincipal> AuthenticateAsync(Credentials credentials,
            CancellationToken cancellationToken)
        {
            var claims = await _permissionProvider.GetClaims(credentials, cancellationToken);
            return claims == null ? null : new DefaultPrincipal(claims)
            {
                Permissions = _permissionProvider.Permissions,
                User = _permissionProvider.User
            };
        }


    }
}