namespace ProductionMan.Data.Models.Membership
{

    public class Permission
    {

        public enum OperationType
        {
            Create,
            Read,
            Update,
            Delete
        }

    
        public int Id { get; set; }

        
        public string ResourceName { get; set; }


        public OperationType Operation { get; set; }
    }
}
