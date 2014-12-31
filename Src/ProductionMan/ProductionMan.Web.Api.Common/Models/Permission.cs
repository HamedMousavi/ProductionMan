using System;

namespace ProductionMan.Web.Api.Common.Models
{
    public class Permission : Linkable
    {
        [Flags]
        public enum OperationType
        {
            Create = 1,
            Read = 2,
            Update = 4,
            Delete = 8
        }
        
        public int PermissionId { get; set; }


        public string ResourceName { get; set; }


        public OperationType Operation { get; set; }


        public string Description { get; set; }
    }
}
