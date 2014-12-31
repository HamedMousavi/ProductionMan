using ProductionMan.Web.Api.Security.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ProductionMan.Web.Api.Security
{
    public interface IPermissionProvider
    {
        Task<ClaimList> GetClaims(Credentials credentials, CancellationToken cancellationToken);

        Task<ClaimList> GetCachedClaims();
    }
}