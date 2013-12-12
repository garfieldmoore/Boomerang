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
        /// <param name="host">Configuration handler for proxy server</param>
        /// <param name="relativeAddress">Relative uri of the request</param>
        /// <returns>Configuration handler</returns>
        public static IBoomerang Get(this IBoomerang host, string relativeAddress)
        {
            var requestResponse = new Request { Address = relativeAddress, Method = "GET" };
            ((BoomarangImpl)host).AddAddress(requestResponse);
            return host;
        }

        /// <summary>
        /// Add a POST request to be intercepted at the given address
        /// </summary>
        /// <param name="host">Configuration handler for proxy server</param>
        /// <param name="relativeAddress">Relative uri of the request</param>
        /// <returns>Configuration handler</returns>
        public static IBoomerang Post(this IBoomerang host, string relativeAddress)
        {
            host.Request(relativeAddress, "POST");
            return host;
        }

        /// <summary>
        /// Add a PUT request to be intercepted at the given address
        /// </summary>
        /// <param name="target">Configuration handler for proxy server</param>
        /// <param name="relativeAddress">Relative uri of the request</param>
        /// <returns>Configuration handler</returns>
        public static IBoomerang Put(this IBoomerang target, string relativeAddress)
        {
            target.Request(relativeAddress, "PUT");
            return target;
        }

        /// <summary>
        /// Add a DELETE request to be intercepted at the given address
        /// </summary>
        /// <param name="target">Configuration handler for proxy server</param>
        /// <param name="relativeAddress">Relative uri of the request</param>
        /// <returns>Configuration handler</returns>
        public static IBoomerang Delete(this IBoomerang target, string relativeAddress)
        {
            target.Request(relativeAddress, "DELETE");
            return target;
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
            return ((BoomarangImpl)target).GetAllReceivedRequests();
        }
    }
}