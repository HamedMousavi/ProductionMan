using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using ProductionMan.Data;
using ProductionMan.Data.MsAdo;
using ProductionMan.Web.Api.ActionResults;
using ProductionMan.Web.Api.Common.Models;
using System.Collections.Generic;
using System.Web.Http;
using ProductionMan.Web.Api.Security;


namespace ProductionMan.Web.Api.Controllers
{

    public class UsersController : ApiController
    {

        private Data.MembershipRepository _repository;


        public UsersController(MembershipRepository repository)
        {
            _repository = repository;
        }


        public IEnumerable<User> GetUsers()
        {
//new Data.MembershipRepository(UnitOfWorkFactory.Create());

            var list = _repository.GetUsers(string.Empty);

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


        [HttpPost]
        public IHttpActionResult AddUser(HttpRequestMessage requestMessage, Data.Shared.Models.User newUser)
        {
            _repository.Insert(newUser);
            var user = new User
            {
                Culture = newUser.Culture,
                Id = newUser.Id,
                Name = newUser.Name,
                Role = newUser.Role
            };

            return new CreatedActionResult<User>(requestMessage, user);
        }
    }
}
