using System.Reflection;
using System.Resources;
using System.Threading;


namespace ProductionMan.Desktop.Services
{

    public class SharedApplicationServices
    {

        private static readonly object _syncLock = new object();
        private static SharedApplicationServices _instance;
        private ResourceManager _resMan;


        public static SharedApplicationServices Instanse
        {
            get
            {
                lock (_syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new SharedApplicationServices();
                    }
                }

                return _instance;
            }
        }


        public SynchronizationContext SynchronizationContext { get; set; }


        internal string GetTextResource(string resourceName)
        {
            //return ResMan.GetString(resourceName);
            var result = string.Empty;

            SynchronizationContext.Send(state =>
                result = ResMan.GetString(resourceName), null);

            return result;
        }


        private ResourceManager ResMan
        {
            get { return _resMan ?? (_resMan = new ResourceManager("ProductionMan.Desktop.Localized.Resources", Assembly.GetExecutingAssembly())); }
        }
    }
}
