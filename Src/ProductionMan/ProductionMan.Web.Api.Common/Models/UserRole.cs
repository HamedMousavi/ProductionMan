namespace ProductionMan.Web.Api.Common.Models
{
    public class UserRole
    {
        public int RoleId { get; set; }

        public string RoleName { get; set; }
       
        public override string ToString()
        {
            return RoleName;
        }
    }
}
