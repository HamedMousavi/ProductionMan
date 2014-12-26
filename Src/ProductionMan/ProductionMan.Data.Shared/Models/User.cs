using System.ComponentModel.DataAnnotations;

namespace ProductionMan.Data.Shared.Models
{

    public class User
    {

        [Key]
        public long UserId { get; set; }

        public string DisplayName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Culture { get; set; }

        public Role Role { get; set; }
    }
}