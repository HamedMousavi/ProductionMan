namespace ProductionMan.Domain.Globalization
{
    public class Language
    {
        public string Name { get; set; }

        public string LocaleName { get; set; }

        public int Id { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
