namespace ProductionMan.Web.Api.Common.Models
{

    public class UserWrite : UserRead
    {

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
