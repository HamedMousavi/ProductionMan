using System.Collections.Generic;
using ProductionMan.Data;
using ProductionMan.Data.MsAdo;
using ProductionMan.Data.Shared.Models;


namespace ProductionMan.Web.Api.Logic
{

    // Control access to data layer. Perhaps load balancing can be applied
    // in order to control number of concurrent connections to data layer &
    // the like
    public sealed class DataProxy
    {

        private DataProxy()
        {
            _methods = new Dictionary<Permission.OperationType, string>
            {
                {Permission.OperationType.Create, "post"},
                {Permission.OperationType.Read, "get"},
                {Permission.OperationType.Update, "put"},
                {Permission.OperationType.Delete, "delete"},
            };
        }


        private static DataProxy _instance;
        private static readonly object _instanceLock = new object();
        private MembershipRepository _membershipRepository;
        private readonly Dictionary<Permission.OperationType, string> _methods;


        public string GetMethodByPermission(Permission.OperationType permission)
        {
            return _methods[permission];
        }


        public static DataProxy Instance
        {
            get
            {
                lock (_instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new DataProxy();
                    }
                }

                return _instance;
            }
        }


        public MembershipRepository MembershipRepository
        {
            get
            {
                return _membershipRepository ??
                       (_membershipRepository = new MembershipRepository(UnitOfWorkFactory.Create()));
            }
        }
    
    
    }
}