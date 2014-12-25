using ProductionMan.Web.Api.Common;
using ProductionMan.Web.Api.Common.Models;
using System;
using System.Linq;


namespace ProductionMan.Web.Api.ActionResults
{
    public static class LocationLinkCalculator
    {
        public static Uri GetLocationLink(ILinkable linkContaining)
        {
            var locationLink = linkContaining.Links.FirstOrDefault(x => x.Rel == Constants.CommonLinkRelValues.Self);
            return locationLink == null ? null : new Uri(locationLink.Href);
        }
    }
}