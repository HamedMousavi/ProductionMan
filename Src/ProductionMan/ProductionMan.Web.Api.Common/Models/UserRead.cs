namespace ProductionMan.Web.Api.Common.Models
{
    public class UserRead : Linkable
    {
        public long UserId { get; set; }

        public string DisplayName { get; set; }

        public string Culture { get; set; }

        public UserRole Role { get; set; }
        
        public override string ToString()
        {
            return string.Format("{0}", DisplayName);
        }
    }
}
