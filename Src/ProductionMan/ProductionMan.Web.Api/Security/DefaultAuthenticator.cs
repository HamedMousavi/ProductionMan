using ProductionMan.Data.MsAdo;
using ProductionMan.Web.Api.Security.Models;
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
            var data = new Data.MembershipRepository(UnitOfWorkFactory.Create());
            var user = data.Find(credentials.Username, credentials.Password);

            if (user != null)
            {
                var claims = new ClaimList
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Uri, "get:api/users")
                };

                return Task.FromResult(claims);
            }

            return Task.FromResult<ClaimList>(null);
        }
    }
}