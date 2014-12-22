using System;
using System.Linq;
using System.Threading;
using ProductionMan.Data.MsAdo;
using ProductionMan.Web.Api.Common.Models;
using System.Collections.Generic;
using System.Web.Http;
using ProductionMan.Web.Api.Security;


namespace ProductionMan.Web.Api.Controllers
{

    public class UsersController : ApiController
    {

        public IEnumerable<User> GetUsers()
        {
            var data = new Data.MembershipRepository(UnitOfWorkFactory.Create());

            var list = data.GetUsers(string.Empty);

            return list.Select(item => new User
            {
                Id = item.Id, Name = item.Name, Links = new LinkList {new Link {Href = string.Format("/api/Users/{0}", item.Id), Method = "GET", Rel = "Self"}}
            }).ToList();
        }


        // Get: api/Users/Current (id=Current)
        public User GetUser(string id)
        {
            if (string.Equals(id, "Current", StringComparison.InvariantCultureIgnoreCase))
            {
                var principal = Thread.CurrentPrincipal as DefaultPrincipal;
                if (principal != null)
                {
                    return new User {Name = principal.Name};
                }
            }

            return null;
        }
    }
}
