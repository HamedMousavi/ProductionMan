using AutoMapper;
using ProductionMan.Web.Api.ActionResults;
using ProductionMan.Web.Api.Common.Models;
using ProductionMan.Web.Api.Logic;
using ProductionMan.Web.Api.Security;
using ProductionMan.Web.Api.Security.Validation;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Web.Http;


namespace ProductionMan.Web.Api.Controllers
{

    [RoutePrefix("api")]
    public class UsersController : ApiController
    {

        [Route("Users")]
        [HttpGet]
        public IEnumerable<UserRead> GetUsers()
        {
            var list = DataProxy.Instance.MembershipRepository.GetUsers(string.Empty);

            return list.Select(Mapper.Map<UserRead>).ToList();
        }


        [Route("Users")]
        [HttpPost]
        [ValidateModel]
        public IHttpActionResult AddUser(HttpRequestMessage requestMessage, UserWrite newUser)
        {
            var user = Mapper.Map<Data.Shared.Models.User>(newUser);

            DataProxy.Instance.MembershipRepository.InsertUser(user);

            var userRead = Mapper.Map<UserRead>(user);

            return new CreatedActionResult<UserRead>(requestMessage, userRead);
        }


        [Route("Users")]
        [HttpPut]
        [ValidateModel]
        public UserRead UpdateUser(HttpRequestMessage requestMessage, UserWrite newUser)
        {
            var user = Mapper.Map<Data.Shared.Models.User>(newUser);

            DataProxy.Instance.MembershipRepository.UpdateUser(user);

            return Mapper.Map<UserRead>(user);
        }


        [Route("Users/{userId:long}")]
        [HttpDelete]
        public IHttpActionResult DeleteUser(long userId)
        {
            DataProxy.Instance.MembershipRepository.DeleteUser(userId);
            return Ok();
        }
    }
}
