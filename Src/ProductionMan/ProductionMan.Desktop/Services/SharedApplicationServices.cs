using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace ProductionMan.Desktop.Services
{

    public class SharedApplicationServices
    {

        private static readonly object _syncLock = new object();
        private static SharedApplicationServices _instance;


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
    }
}
