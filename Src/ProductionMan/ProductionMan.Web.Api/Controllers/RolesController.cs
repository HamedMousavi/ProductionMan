using AutoMapper;
using ProductionMan.Data.Shared.Models;
using ProductionMan.Web.Api.ActionResults;
using ProductionMan.Web.Api.Common.Models;
using ProductionMan.Web.Api.Logic;
using ProductionMan.Web.Api.Security.Validation;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;


namespace ProductionMan.Web.Api.Controllers
{

    public class RolesController : ApiController
    {

        [HttpGet]
        public IEnumerable<UserRole> Read()
        {
            var list = DataProxy.Instance.MembershipRepository.GetRoles(string.Empty);

            return list.Select(Mapper.Map<UserRole>).ToList();
        }


        [HttpPost]
        [ValidateModel]
        public IHttpActionResult Insert(HttpRequestMessage requestMessage, UserRole userRole)
        {
            var role = Mapper.Map<Role>(userRole);

            DataProxy.Instance.MembershipRepository.InsertRole(role);

            userRole = Mapper.Map<UserRole>(role);

            return new CreatedActionResult<UserRole>(requestMessage, userRole);
        }


        [HttpPut]
        [ValidateModel]
        public UserRole Update(HttpRequestMessage requestMessage, UserRole userRole)
        {
            var role = Mapper.Map<Role>(userRole);

            DataProxy.Instance.MembershipRepository.UpdateRole(role);

            return Mapper.Map<UserRole>(role);
        }


        [HttpDelete]
        public IHttpActionResult Delete(long id)
        {
            DataProxy.Instance.MembershipRepository.DeleteRole(id);
            return Ok();
        }
    }
}
