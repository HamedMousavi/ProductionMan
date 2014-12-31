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

    public class UsersController : ApiController
    {

        // Get: api/Users
        [HttpGet]
        public IEnumerable<UserRead> GetUsers()
        {
            var list = DataProxy.Instance.MembershipRepository.GetUsers(string.Empty);

            return list.Select(Mapper.Map<UserRead>).ToList();
        }


        // Get: api/Users/Current (id=Current)
        //[HttpGet]
        //public UserRead GetUser(string id)
        //{
        //    if (string.Equals(id, "Current", StringComparison.InvariantCultureIgnoreCase))
        //    {
        //        var principal = Thread.CurrentPrincipal as DefaultPrincipal;
        //        if (principal != null)
        //        {
        //            return new UserRead {DisplayName = principal.Name};
        //        }
        //    }
        //    else if (string.Equals(id, "Roles", StringComparison.InvariantCultureIgnoreCase))
        //    {
        //        var list = DataProxy.Instance.MembershipRepository.GetRoles(string.Empty);

        //        //return list.Select(Mapper.Map<UserRole>).ToList();
        //    }

        //    return null;
        //}


        [HttpPost]
        [ValidateModel]
        public IHttpActionResult AddUser(HttpRequestMessage requestMessage, UserWrite newUser)
        {
            var user = Mapper.Map<User>(newUser);

            DataProxy.Instance.MembershipRepository.InsertUser(user);

            var userRead = Mapper.Map<UserRead>(user);

            return new CreatedActionResult<UserRead>(requestMessage, userRead);
        }


        [HttpPut]
        [ValidateModel]
        public UserRead UpdateUser(HttpRequestMessage requestMessage, UserWrite newUser)
        {
            var user = Mapper.Map<User>(newUser);

            DataProxy.Instance.MembershipRepository.UpdateUser(user);

            return Mapper.Map<UserRead>(user);
        }


        [HttpDelete]
        public IHttpActionResult DeleteUser(long id)
        {
            DataProxy.Instance.MembershipRepository.DeleteUser(id);
            return Ok();
        }
    }
}
