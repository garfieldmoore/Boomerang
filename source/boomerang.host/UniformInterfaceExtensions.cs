namespace Rainbow.Testing.Boomerang.Host
{
    using System.Collections.Generic;

    /// <summary>
    /// Helper methods to configure requests to intercept and desired responses from proxy server
    /// </summary>
    public static class UniformInterfaceExtensions
    {
        /// <summary>
        /// Add a GET request to be intercepted
        /// </summary>
        /// <param name="host">Configuration handler for proxer server</param>
        /// <param name="prefix">Address of GET request relative to the base address and port the proxy server was started on</param>
        /// <returns>Configuration handler</returns>
        public static IBoomerang Get(this IBoomerang host, string prefix)
        {
            var requestResponse = new Request { Address = prefix, Method = "GET" };
            ((BoomarangImpl)host).AddAddress(requestResponse);
            return host;
        }

        /// <summary>
        /// Add a POST request to be intercepted at the given address
        /// </summary>
        /// <param name="host">Configuration handler for proxer server</param>
        /// <param name="relativeAddress">Address of request relative to the base address and port the proxy server is listening on</param>
        /// <returns>Configuration handler</returns>
        public static IBoomerang Post(this IBoomerang host, string relativeAddress)
        {
            host.Request(relativeAddress, "POST");
            return host;
        }

        /// <summary>
        /// Add a PUT request to be intercepted at the given address
        /// </summary>
        /// <param name="target">Configuration handler for proxer server</param>
        /// <param name="relativeAddress">Address of request relative to the base address and port the proxy server is listening on</param>
        /// <returns>Configuration handler</returns>
        public static IBoomerang Put(this IBoomerang target, string relativeAddress)
        {
            target.Request(relativeAddress, "PUT");
            return target;
        }

        /// <summary>
        /// Add a DELETE request to be intercepted at the given address
        /// </summary>
        /// <param name="target">Configuration handler for proxer server</param>
        /// <param name="relativeAddress">Address of request relative to the base address and port the proxy server is listening on</param>
        /// <returns>Configuration handler</returns>
        public static IBoomerang Delete(this IBoomerang target, string relativeAddress)
        {
            target.Request(relativeAddress, "DELETE");
            return target;
        }

        /// <summary>
        /// Add a request to be intercepted at the given relative address
        /// </summary>
        /// <param name="host">Configuration handler for proxer server</param>
        /// <param name="relativeAddress">Address of request relative to the base address and port the proxy server is listening on</param>
        /// <param name="httpMethod">The method of the request to register on this address</param>
        /// <returns>Configuration handler</returns>
        public static IBoomerang Request(this IBoomerang host, string relativeAddress, string httpMethod)
        {
            var requestResponse = new Request { Address = relativeAddress, Method = httpMethod };
            ((BoomarangImpl)host).AddAddress(requestResponse);
            return host;
        }

        /// <summary>
        /// Set the response to return on the previously added request address
        /// </summary>
        /// <param name="host">Configuration handler for proxer server</param>
        /// <param name="body">The required response body</param>
        /// <param name="statusCode">The required status code for the response</param>
        /// <returns>Configuration handler</returns>
        public static IBoomerang Returns(this IBoomerang host, string body, int statusCode)
        {
            ((BoomarangImpl)host).AddResponse(body, statusCode);
            return host;
        }

        /// <summary>
        /// Returns all requests that were recieved by the proxy server
        /// </summary>
        /// <param name="target">Configuration handler for proxer server</param>
        /// <returns>List of requests</returns>
        public static IEnumerable<Request> GetAllReceivedRequests(this IBoomerang target)
        {
            return ((BoomarangImpl)target).GetAllReceivedRequests();
        }

    }
}