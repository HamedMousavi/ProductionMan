using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using ProductionMan.Web.Api.Common.Models;
using ProductionMan.Web.Api.Logic;


namespace ProductionMan.Web.Api.Controllers
{

    [RoutePrefix("api")]
    public class PermissionsController : ApiController
    {
        [Route("Permissions")]
        [HttpGet]
        public IEnumerable<Permission> GetPermission()
        {
            var list = DataProxy.Instance.MembershipRepository.GetPermissions(string.Empty);

            return list.Select(Mapper.Map<Permission>).ToList();
        }
    }
}






