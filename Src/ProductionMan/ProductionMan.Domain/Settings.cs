namespace ProductionMan.Domain
{
    public static class Settings
    {
        private static string _serverBaseAddress;

        public static string ServerBaseAddress
        {
            get
            {
                return string.IsNullOrWhiteSpace(_serverBaseAddress)
                    ? "http://localhost/ProductionMan/api/"
                    : _serverBaseAddress;
            }

            set { _serverBaseAddress = value; }
        }
    }
}
