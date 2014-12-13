using ProductionMan.Web.Api.Common.Models;
using System.Collections.Generic;
using System.Web.Http;


namespace ProductionMan.Web.Api.Controllers
{

    public class UsersController : ApiController
    {
        public List<User> GetUsers()
        {
            var list = new List<User> {new User {Id = 0, Name = "Hamed"}};

            return list;
        }
    }
}
