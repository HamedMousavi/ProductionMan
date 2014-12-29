using AutoMapper;
using ProductionMan.Data.Shared.Models;
using ProductionMan.Web.Api.ActionResults;
using ProductionMan.Web.Api.Common.Models;
using ProductionMan.Web.Api.Logic;
using ProductionMan.Web.Api.Security;
using ProductionMan.Web.Api.Security.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Web.Http;


namespace ProductionMan.Web.Api.Controllers
{

    public class UsersController : ApiController
    {

        public IEnumerable<UserRead> GetUsers()
        {
            var list = DataProxy.Instance.MembershipRepository.GetUsers(string.Empty);

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
        [ValidateModel]
        public IHttpActionResult AddUser(HttpRequestMessage requestMessage, UserWrite newUser)
        {
            var user = Mapper.Map<User>(newUser);

            DataProxy.Instance.MembershipRepository.Insert(user);

            var userRead = Mapper.Map<UserRead>(user);

            return new CreatedActionResult<UserRead>(requestMessage, userRead);
        }


        [HttpDelete]
        public IHttpActionResult DeleteUser(long id)
        {
            DataProxy.Instance.MembershipRepository.DeleteUser(id);
            return Ok();
        }
    }
}
