using System.Collections.Generic;


namespace ProductionMan.Data.Shared.Models
{

    public class Role : UserRole
    {
        public List<Permission> Permissions { get; set; }

    }
}
