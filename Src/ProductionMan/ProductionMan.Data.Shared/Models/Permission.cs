using System;
using System.ComponentModel.DataAnnotations;

namespace ProductionMan.Data.Shared.Models
{

    public class Permission
    {

        [Flags]
        public enum OperationType
        {
            Create  = 1,
            Read    = 2,
            Update  = 4,
            Delete  = 8
        }


        [Key]
        public int PermissionId { get; set; }

        
        public string ResourceName { get; set; }


        public OperationType Operation { get; set; }
        
        
        public string Description { get; set; }
    }
}
