using AutoMapper;
using ProductionMan.Data.MsAdo;
using ProductionMan.Data.Shared.Models;
using ProductionMan.Web.Api.ActionResults;
using ProductionMan.Web.Api.Common.Models;
using ProductionMan.Web.Api.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using ProductionMan.Web.Api.Security.Validation;


namespace ProductionMan.Web.Api.Controllers
{

    public class UsersController : ApiController
    {

        private readonly Data.MembershipRepository _repository;


        public UsersController()
        {
            _repository = new Data.MembershipRepository(UnitOfWorkFactory.Create());
        }


        public IEnumerable<UserRead> GetUsers()
        {
            var list = _repository.GetUsers(string.Empty);

            return list.Select(Mapper.Map<UserRead>).ToList();
        }

            //var result = new List<UserRead>();
            //foreach (var user in list)
            //{
            //    result.Add(Mapper.Map<UserRead>(user));
            //}
            //return list.Select(item => new UserRead
            //{
            //    UserId = item.UserId, DisplayName = item.DisplayName, Links = new LinkList {new Link {Href = string.Format("/api/Users/{0}", item.UserId), Method = "GET", Rel = "Self"}}
            //}).ToList();


        // Get: api/Users/Current (id=Current)
        public UserRead GetUser(string id)
        {
            if (string.Equals(id, "Current", StringComparison.InvariantCultureIgnoreCase))
            {
                var principal = Thread.CurrentPrincipal as DefaultPrincipal;
                if (principal != null)
                {
                    return new UserRead {DisplayName = principal.Name};
                }
            }

            return null;
        }


        [ValidateModel]
        [HttpPost]
        public IHttpActionResult AddUser(HttpRequestMessage requestMessage, UserWrite newUser)
        {
            var user = Mapper.Map<User>(newUser);

            _repository.Insert(user);

            var userRead = Mapper.Map<UserRead>(user);

            return new CreatedActionResult<UserRead>(requestMessage, userRead);
        }
    }
}
