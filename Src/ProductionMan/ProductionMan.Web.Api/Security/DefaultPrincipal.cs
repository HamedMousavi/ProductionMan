﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using ProductionMan.Web.Api.Common.Models;


namespace ProductionMan.Web.Api.Security
{

    public class DefaultPrincipal : ClaimsPrincipal
    {

        private readonly IEnumerable<Claim> _claims;


        public DefaultPrincipal(IReadOnlyCollection<Claim> claims)
            : base(new[] { new ClaimsIdentity(claims, "Token") })
        {
            _claims = claims;
        }


        public IEnumerable<Common.Models.Permission> Permissions { get; set; }

        public UserRead User { get; set; }


        public string Name
        {
            get
            {
                foreach (var claim in _claims)
                {
                    if (string.Equals(claim.Type, ClaimTypes.Name, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return claim.Value;
                    }
                }

                return string.Empty;
            }
        }

    }
}