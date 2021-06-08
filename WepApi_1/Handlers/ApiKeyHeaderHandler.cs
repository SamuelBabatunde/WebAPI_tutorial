using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace WepApi_1.Handlers
{
    public class ApiKeyHeaderHandler : DelegatingHandler
    {
        /// <summary>
        /// Name of our custom header to look for
        /// </summary>
        public const string _apiKeyHeader = "X-API-Key";

        /// <summary>
        ///  Name of api key query string key
        /// </summary>
        public const string _apiQueryString = "api_key";

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // STEP 1: Global message-level logic that must be executed BEFORE the request
            //          is sent on to the action method
            string apikey = null;

            //if (request.RequestUri.Segments[1].ToLowerInvariant().StartsWith("swagger"))
            //    return base.SendAsync(request, cancellationToken);
            //or
            //if (request.RequestUri.ToString().ToLower().Contains("swagger"))
            //{

            //    return await base.SendAsync(request, cancellationToken);
            //}

            if (request.Headers.Contains(_apiKeyHeader))
            {
                apikey = request.Headers.GetValues(_apiKeyHeader).FirstOrDefault();
            }
            else
            {
                // let's see if it is on the query string instead
                var queryString = request.GetQueryNameValuePairs();
                var kvp = queryString.FirstOrDefault(a => a.Key.ToLowerInvariant().Equals(_apiQueryString));
                if (!string.IsNullOrEmpty(kvp.Value))
                    apikey = kvp.Value;
            }

            // was any api key present?  If none, abort request
            //This shouldn't be here, uthorization should be done in a Authorization Filter
            //if (string.IsNullOrEmpty(apikey))
            //{
            //    // Create the response.
            //    var errorResponse = new HttpResponseMessage(HttpStatusCode.Forbidden)
            //    {
            //        Content = new StringContent("Missing API key")
            //    };

            //    return await Task.FromResult(errorResponse);
            //}

            // save the value to Properties; 
            request.Properties.Add(_apiKeyHeader, apikey);
            request.Headers.Add(_apiKeyHeader,apikey);

            // compress steps 2, 3 and 4 into one line since we don't need any post-request processing
            var response = await base.SendAsync(request, cancellationToken);
            response.Headers.Add(_apiKeyHeader, apikey);

            return response;
        }
    }
}