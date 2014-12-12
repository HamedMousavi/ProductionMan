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
            return new ClaimsPrincipal(new[] {new ClaimsIdentity(claims, "Token")});
        }


        private Task<ClaimList> LoadPermissions(Credentials credentials, CancellationToken cancellationToken)
        {
            var claims = new ClaimList
            {
                new Claim(ClaimTypes.Name, "hamed"),
            };

            if (credentials.Username == "1" && credentials.Password == "1")
            {
                claims.Add(new Claim(ClaimTypes.Uri, "api/users"));
            }

            return Task.FromResult(claims);
        }
    }
}