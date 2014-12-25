using System.ComponentModel.DataAnnotations;


namespace ProductionMan.Data.Shared.Models
{

    public class User
    {

        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Culture { get; set; }

        public UserRole Role { get; set; }

        public override string ToString()
        {
            return string.Format("{0}", Name);
        }
    }
}