namespace Rainbow.Testing.Boomerang.Host
{
    using System.Collections.Generic;

    using System.Linq;

    /// <summary>
    /// Helper methods to configure requests to intercept and desired responses from proxy server
    /// </summary>
    public static class UniformInterfaceExtensions
    {
        private static bool collectingEvents;
        private static object locker = new object();
        private static IList<Request> ReceivedRequests = new List<Request>();


        /// <summary>
        /// Add a GET request to be intercepted
        /// </summary>
        /// <param name="host">Configuration handler for proxy server</param>
        /// <param name="relativeAddress">Relative uri of the request</param>
        /// <returns>Configuration handler</returns>
        public static IBoomerang Get(this IBoomerang host, string relativeAddress)
        {
            return Request(host, relativeAddress, "GET");
        }

        /// <summary>
        /// Add a POST request to be intercepted at the given address
        /// </summary>
        /// <param name="host">Configuration handler for proxy server</param>
        /// <param name="relativeAddress">Relative uri of the request</param>
        /// <returns>Configuration handler</returns>
        public static IBoomerang Post(this IBoomerang host, string relativeAddress)
        {
            return Request(host, relativeAddress, "POST");
        }

        /// <summary>
        /// Add a PUT request to be intercepted at the given address
        /// </summary>
        /// <param name="target">Configuration handler for proxy server</param>
        /// <param name="relativeAddress">Relative uri of the request</param>
        /// <returns>Configuration handler</returns>
        public static IBoomerang Put(this IBoomerang target, string relativeAddress)
        {
            return Request(target, relativeAddress, "PUT");
        }

        /// <summary>
        /// Add a DELETE request to be intercepted at the given address
        /// </summary>
        /// <param name="target">Configuration handler for proxy server</param>
        /// <param name="relativeAddress">Relative uri of the request</param>
        /// <returns>Configuration handler</returns>
        public static IBoomerang Delete(this IBoomerang target, string relativeAddress)
        {
            return Request(target, relativeAddress, "DELETE");
        }

        /// <summary>
        /// Add a request to be intercepted at the given relative address
        /// </summary>
        /// <param name="host">Configuration handler for proxy server</param>
        /// <param name="relativeAddress">Relative uri of the request</param>
        /// <param name="httpMethod">The method of the request to register on this address</param>
        /// <returns>Configuration handler</returns>
        /// <remarks>This should only be used for unsupported HTTP requests. Use the extension methods for supported requests</remarks>
        /// <seealso cref="Get"/>
        /// <seealso cref="Put"/>
        /// <seealso cref="Post"/>
        /// <seealso cref="Delete"/>
        public static IBoomerang Request(this IBoomerang host, string relativeAddress, string httpMethod)
        {
            CollectEvents(host);

            var requestResponse = new Request { Address = relativeAddress, Method = httpMethod };
            ((BoomarangImpl)host).AddAddress(requestResponse);
            return host;
        }

        /// <summary>
        /// Set the response to return on the previously added request address
        /// </summary>
        /// <param name="host">Configuration handler for proxy server</param>
        /// <param name="body">The required response body</param>
        /// <param name="statusCode">The required status code for the response</param>
        /// <returns>Configuration handler</returns>
        public static IBoomerang Returns(this IBoomerang host, string body, int statusCode)
        {
            ((BoomarangImpl)host).AddResponse(body, statusCode);
            return host;
        }

        /// <summary>
        /// Set the response to return on the previously added request address
        /// </summary>
        /// <param name="host">Configuration handler for proxy server</param>
        /// <param name="body">The required response body</param>
        /// <param name="statusCode">The required status code for the response</param>
        /// <param name="headers">Headers to set in response</param>
        /// <returns>Configuration handler</returns>
        public static IBoomerang Returns(
            this IBoomerang host, string body, int statusCode, IDictionary<string, string> headers)
        {
            ((BoomarangImpl)host).AddResponse(body, statusCode, headers);
            return host;
        }

        /// <summary>
        /// Returns all requests that were received by the proxy server
        /// </summary>
        /// <param name="target">Configuration handler for proxy server</param>
        /// <returns>List of requests</returns>
        /// <seealso cref="Request"/>
        public static IEnumerable<Request> GetAllReceivedRequests(this IBoomerang target)
        {
            return ReceivedRequests.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static void ClearReceivedRequests(this IBoomerang target)
        {
            ReceivedRequests.Clear();
        }

        private static void CollectEvents(IBoomerang host)
        {
            if (!collectingEvents)
            {
                lock (locker)
                {
                    if (!collectingEvents)
                    {
                        collectingEvents = true;
                        host.OnReceivedRequest += host_OnReceivedRequest;
                    }
                }
            }
        }

        private static void host_OnReceivedRequest(object sender, ProxyRequestEventArgs e)
        {
            ReceivedRequests.Add(new Request() { Method = e.Method, Address = e.RelativePath, Body = e.Body });
        }
    }
}