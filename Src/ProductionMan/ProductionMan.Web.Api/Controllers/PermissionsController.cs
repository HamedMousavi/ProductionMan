using System.Collections.Generic;
using System.Web.Http;
using ProductionMan.Web.Api.Common.Models;


namespace ProductionMan.Web.Api.Controllers
{

    public class PermissionsController : ApiController
    {
        public IEnumerable<Permission> GetPermission()
        {
            //var data = new Data.MembershipRepository(UnitOfWorkFactory.Create());

            //var list = data.GetUsers(string.Empty);

            //return list.Select(item => new User
            //{
            //    Id = item.Id,
            //    Name = item.Name,
            //    Links = new LinkList { new Link { Href = string.Format("/api/Users/{0}", item.Id), Method = "GET", Rel = "Self" } }
            //}).ToList();

            return new List<Permission>
            {
                new Permission {Operation = Permission.OperationType.Read | Permission.OperationType.Create, ResourceName = "Users" },
                new Permission {Operation = Permission.OperationType.Read | Permission.OperationType.Create, ResourceName = "Permissions" },
                new Permission {Operation = Permission.OperationType.Read | Permission.OperationType.Create, ResourceName = "Settings" },
                new Permission {Operation = Permission.OperationType.Read | Permission.OperationType.Create, ResourceName = "About" },
            };
        }
    }
}






