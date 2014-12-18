﻿using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ProductionMan.Web.Api.Common.Models;


namespace ProductionMan.Domain.WebServices
{

    public class Membership : WebServiceBase
    {

        private const string PermissionsUrl = "Permissions";


        public async Task<ServiceCallResult<List<Permission>>> GetPermissions()
        {
            var result = new ServiceCallResult<List<Permission>>();

            using (var client = new HttpClient())
            {
                PrepareHeaders(client, true);

                var response = await client.GetAsync(PermissionsUrl);
                result.CallStatusCode = response.StatusCode;
                result.CallStatusMessage = response.ReasonPhrase;
                if (response.IsSuccessStatusCode)
                {
                    result.Results = response.Content.ReadAsAsync<List<Permission>>().Result;
                }
            }

            return result;
        }
    }
}
