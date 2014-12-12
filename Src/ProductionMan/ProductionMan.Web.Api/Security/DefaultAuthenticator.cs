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
            return claims == null ? null : new ClaimsPrincipal(new[] {new ClaimsIdentity(claims, "Token")});
        }


        private static Task<ClaimList> LoadPermissions(Credentials credentials, CancellationToken cancellationToken)
        {

            if (credentials.Username == "1" && credentials.Password == "1")
            {
                var claims = new ClaimList
                {
                    new Claim(ClaimTypes.Name, "hamed"),
                    new Claim(ClaimTypes.Uri, "api/users")
                };

                return Task.FromResult(claims);
            }

            return Task.FromResult<ClaimList>(null);
        }
    }
}