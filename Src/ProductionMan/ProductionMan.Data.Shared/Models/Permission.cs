using System;


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


        public int Id { get; set; }

        
        public string ResourceName { get; set; }


        public OperationType Operation { get; set; }
    }
}
