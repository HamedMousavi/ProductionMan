using System.ComponentModel.DataAnnotations;


namespace ProductionMan.Web.Api.Common.Models
{
    public class UserRead : Linkable
    {
        
        [Key]
        public long UserId { get; set; }


        [Required(AllowEmptyStrings = false)]
        public string DisplayName { get; set; }


        public string Culture { get; set; }


        [Required]
        public UserRole Role { get; set; }
        

        public override string ToString()
        {
            return string.Format("{0}", DisplayName);
        }
    }
}
