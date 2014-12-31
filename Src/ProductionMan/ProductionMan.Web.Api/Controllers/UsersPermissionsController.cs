using ProductionMan.Web.Api.Common.Models;
using ProductionMan.Web.Api.Security;
using System.Collections.Generic;
using System.Threading;
using System.Web.Http;


namespace ProductionMan.Web.Api.Controllers
{

    [RoutePrefix("api")]
    public class UsersPermissionsController : ApiController
    {
        [Route("Users/{userId:long}/Permissions")]
        [HttpGet]
        public IEnumerable<Permission> GetUserPermissions(long userId)
        {
            var principal = Thread.CurrentPrincipal as DefaultPrincipal;
            if (principal != null && principal.User.UserId == userId)
            {
                return principal.Permissions;
            }

            return null;
        }
    }
}
