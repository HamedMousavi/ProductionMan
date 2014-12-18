﻿
namespace ProductionMan.Web.Api.Common.Models
{
    public class User : Data.Shared.Models.User, ILinkable
    {

        private LinkList _links;


        public LinkList Links
        {
            get { return _links ?? (_links = new LinkList()); }
            set { _links = value; }
        }

    }
}
