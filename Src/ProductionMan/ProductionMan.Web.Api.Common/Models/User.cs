
namespace ProductionMan.Web.Api.Common.Models
{
    public class User
    {
        private LinkList _links;

        public long Id { get; set; }
    
        public string Name { get; set; }

        public LinkList Links
        {
            get { return _links ?? (_links = new LinkList()); }
            set { _links = value; }
        }
    }
}