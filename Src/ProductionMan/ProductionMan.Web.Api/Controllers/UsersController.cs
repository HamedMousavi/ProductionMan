using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using AutoMapper;
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


        [HttpPost]
        public IHttpActionResult AddUser(HttpRequestMessage requestMessage, Data.Shared.Models.User newUser)
        {
            _repository.Insert(newUser);
            var user = new UserRead
            {
                Culture = newUser.Culture,
                UserId = newUser.UserId,
                DisplayName = newUser.DisplayName,
                Role = new UserRole
                {
                    RoleId = newUser.Role.RoleId,
                    RoleName = newUser.Role.RoleName
                }
            };

            return new CreatedActionResult<UserRead>(requestMessage, user);
        }
    }
}
