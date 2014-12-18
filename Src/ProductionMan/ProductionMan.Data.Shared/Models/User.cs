
namespace ProductionMan.Data.Shared.Models
{
    public class User
    {

        public long Id { get; set; }
    
        public string Name { get; set; }

        public string Culture { get; set; }

        public override string ToString()
        {
            return string.Format("{0}", Name);
        }
    }
}