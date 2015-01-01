using AutoMapper;
using ProductionMan.Web.Api.Common.Models;
using ProductionMan.Web.Api.Logic;
using System.Web.Http;


namespace ProductionMan.Web.Api.Controllers
{

    [RoutePrefix("api")]
    public class RolesPermissionsController : ApiController
    {

        [Route("Roles/{roleId:int}/Permissions/{permissionId:int}")]
        [HttpPut]
        public UserRole AddRolePermission(int roleId, int permissionId)
        {
            DataProxy.Instance.MembershipRepository.AssignPermissionToRole(roleId, permissionId);
            var role = DataProxy.Instance.MembershipRepository.RoleGetById(roleId);
            return Mapper.Map<UserRole>(role);
        }


        [Route("Roles/{roleId:int}/Permissions/{permissionId:int}")]
        [HttpDelete]
        public UserRole DeleteRolePermission(int roleId, int permissionId)
        {
            DataProxy.Instance.MembershipRepository.RetractPermissionFromRole(roleId, permissionId);
            var role = DataProxy.Instance.MembershipRepository.RoleGetById(roleId);
            return Mapper.Map<UserRole>(role);
        }
    }
}
