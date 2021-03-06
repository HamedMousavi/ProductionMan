﻿using ProductionMan.Web.Api.Common.Models;
using ProductionMan.Web.Api.Security;
using System.Threading;
using System.Web.Http;


namespace ProductionMan.Web.Api.Controllers
{

    [RoutePrefix("api")]
    public class ProfilesController : ApiController
    {
        // AS AN EXCEPTION, GET METHOD HERE RETURNS LOGGED IN USER'S PROFILE
        // Get: api/Profiles
        [Route("Profiles")]
        [HttpGet]
        public UserRead GetProfile()
        {
            var principal = Thread.CurrentPrincipal as DefaultPrincipal;
            if (principal != null)
            {
                return principal.User;
            }

            return null;
        }
    }
}
