namespace ProductionMan.Web.Api.Common.Models
{
    public class Linkable : ILinkable
    {

        private LinkList _links;


        public LinkList Links
        {
            get { return _links ?? (_links = new LinkList()); }
            set { _links = value; }
        }
    }
}
