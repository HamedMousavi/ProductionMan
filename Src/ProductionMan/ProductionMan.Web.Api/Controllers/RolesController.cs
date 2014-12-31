using AutoMapper;
using ProductionMan.Web.Api.Common.Models;
using ProductionMan.Web.Api.Logic;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;


namespace ProductionMan.Web.Api.Controllers
{

    public class RolesController : ApiController
    {

        [HttpGet]
        public IEnumerable<UserRole> GetRoles()
        {
            var list = DataProxy.Instance.MembershipRepository.GetRoles(string.Empty);

            return list.Select(Mapper.Map<UserRole>).ToList();
        }
    }
}
