namespace ProductionMan.Data.Shared.Models
{
    public class UserRole
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
