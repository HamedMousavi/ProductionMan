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
            //var data = new Data.MembershipRepository(UnitOfWorkFactory.Create());

            //var list = data.GetUsers(string.Empty);

            //return list.Select(item => new User
            //{
            //    Id = item.Id,
            //    Name = item.Name,
            //    Links = new LinkList { new Link { Href = string.Format("/api/Users/{0}", item.Id), Method = "GET", Rel = "Self" } }
            //}).ToList();

            //return new List<Permission>
            //{
            //    new Permission {Operation = Permission.OperationType.Read | Permission.OperationType.Create, ResourceName = "Users" },
            //    new Permission {Operation = Permission.OperationType.Read | Permission.OperationType.Create, ResourceName = "Permissions" },
            //    new Permission {Operation = Permission.OperationType.Read | Permission.OperationType.Create, ResourceName = "Settings" },
            //    new Permission {Operation = Permission.OperationType.Read | Permission.OperationType.Create, ResourceName = "About" },
            //};
        }
    }
}






