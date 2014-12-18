namespace ProductionMan.Web.Api.Common.Models
{
    public class Permission : Data.Shared.Models.Permission, ILinkable
    {
        #region Links
        
        
        private LinkList _links;


        public LinkList Links
        {
            get { return _links ?? (_links = new LinkList()); }
            set { _links = value; }
        }


        #endregion Links
    }
}
