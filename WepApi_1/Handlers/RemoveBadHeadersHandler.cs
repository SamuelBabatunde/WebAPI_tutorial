using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace WepApi_1.Handlers
{
    public class RemoveBadHeadersHandler : DelegatingHandler
    {
        /// <summary>
        /// Names of header to remove
        /// </summary>
        readonly string[] _badHeaderKeys = { "X-Powered-By", "X-AspNet-Version", "Server" };

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            //STEP 2: Call the rest of the pipeline
            var response = await base.SendAsync(request, cancellationToken);

            //STEP 3: Any global message-level logic that must be executed AFTER the request has executed, 
            //          before the final HTTP response message


            //remove all bad headers from the collection
            foreach (var headerKey in _badHeaderKeys)
            {
                //this does not work, replace <httpRuntime targetFramework="4.6.1"/>
                //with <httpRuntime targetFramework="4.6.1" enableVersionHeader="false"/> in  <system.web>
                //in the web.config file instead
                //Add     <httpProtocol>
                //<customHeaders>
                //<remove name="X-Powered-By"/>
                //</customHeaders>
                //</httpProtocol>
                //<security>
                //<requestFiltering removeServerHeader="true"/>
                // </security>
                // to the   <system.webServer> tag
                response.Headers.Remove(headerKey);
            }


            //STEP 4: Return the final HTTP response
            return response;
        }
    }
}