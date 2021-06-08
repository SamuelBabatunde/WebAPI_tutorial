using System;
using System.Net.Http;
using WepApi_1.Handlers;

namespace WepApi_1.Extensions
{
    /// <summary>
    /// http request extension for retrieving api key if present
    /// </summary>
    public static class HttpRequestMessageExtension
    {
        /// <summary>
        /// Retrieves the Api key present in the request, or null if none found. 
        /// </summary>
        public static string GetApiKey(this HttpRequestMessage request)
        {
            if (request == null)
                return null;

            if (request.Properties.TryGetValue(ApiKeyHeaderHandler._apiKeyHeader, out object apiKey))
            {
                return (string)apiKey;
            }

            return null;
        }

        /// <summary>
        /// Retrieves the base URL to use in order to create a "self-referencing URL", a URL
        /// that points at this web service but from the client's perspective, taking into
        /// account any load balancers in use in front of the service.
        /// </summary>
        /// <remarks>
        /// Assuming the original URL the client browser called was:  https://www.mycompany.com/api/products
        /// but your web service sits behind a load balancer at:  http://myserver/api/products
        /// the self-reference base URL returned by this method would be: https://www.mycompany.com/
        /// 
        /// If no load balancer was in use then the self-reference base URL 
        /// returned by this method would be: http://myserver/ 
        /// </remarks>
        /// <param name="request">HttpRequestMessage object.</param>
        /// <returns>Self-referencing base URL for creating another URL that references the same service,
        /// from the original client caller's perspective.</returns>
        public static Uri GetSelfReferenceBaseUrl(this HttpRequestMessage request)
        {
            if (request == null)
                return null;

            if (request.Properties.TryGetValue(ForwardedHeadersHandler.MyClientBaseUrlProperty,
                out object baseUrl))
            {
                return (Uri)baseUrl;
            }

            return null;
        }

        /// <summary>
        /// Retrieves a URL re-based from the client's perspective, taking into
        /// account any load balancers in use in front of the service, given a server-base URL.
        /// </summary>
        /// <remarks>
        /// See remarks on <see cref="GetSelfReferenceBaseUrl"/>.
        /// </remarks>
        /// <param name="request">HttpRequestMessage object.</param>
        /// <param name="serverUrl">Uri instance of the server-based URL</param>
        /// <returns>Re-based URL from the original client caller's perspective.</returns>
        public static Uri RebaseUrlForClient(this HttpRequestMessage request, Uri serverUrl)
        {
            Uri clientBase = GetSelfReferenceBaseUrl(request);
            if (clientBase == null)
                return null;
            if (serverUrl == null)
                return clientBase;

            // rest the base scheme/host/port to the client version
            var builder = new UriBuilder(serverUrl);
            builder.Scheme = clientBase.Scheme;
            builder.Host = clientBase.Host;
            builder.Port = clientBase.Port;

            return builder.Uri;
        }
    }
}