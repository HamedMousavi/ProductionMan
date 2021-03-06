﻿using System.Collections.Generic;

namespace ProductionMan.Data.Shared.Models
{

    public class Role
    {
        public int RoleId { get; set; }

        public string RoleName { get; set; }
        
        public IEnumerable<Permission> Permissions { get; set; }

    }
}
