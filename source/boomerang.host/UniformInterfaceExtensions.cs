using System;

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
        public static IRequestHandler Get(this IBoomerang host, string relativeAddress)
        {
            return Request(host, relativeAddress, "GET");
        }

        /// <summary>
        /// Add a POST request to be intercepted at the given address
        /// </summary>
        /// <param name="host">Configuration handler for proxy server</param>
        /// <param name="relativeAddress">Relative uri of the request</param>
        /// <returns>Configuration handler</returns>
        public static IRequestHandler Post(this IBoomerang host, string relativeAddress)
        {
            return Request(host, relativeAddress, "POST");
        }

        /// <summary>
        /// Add a PUT request to be intercepted at the given address
        /// </summary>
        /// <param name="target">Configuration handler for proxy server</param>
        /// <param name="relativeAddress">Relative uri of the request</param>
        /// <returns>Configuration handler</returns>
        public static IRequestHandler Put(this IBoomerang target, string relativeAddress)
        {
            return Request(target, relativeAddress, "PUT");
        }

        /// <summary>
        /// Add a DELETE request to be intercepted at the given address
        /// </summary>
        /// <param name="target">Configuration handler for proxy server</param>
        /// <param name="relativeAddress">Relative uri of the request</param>
        /// <returns>Configuration handler</returns>
        public static IRequestHandler Delete(this IBoomerang target, string relativeAddress)
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
        /// <seealso>
        ///     <cref>Get</cref>
        /// </seealso>
        /// <seealso>
        ///     <cref>Put</cref>
        /// </seealso>
        /// <seealso>
        ///     <cref>Post</cref>
        /// </seealso>
        /// <seealso>
        ///     <cref>Delete</cref>
        /// </seealso>
        public static IRequestHandler Request(this IBoomerang host, string relativeAddress, string httpMethod)
        {
            CollectEvents(host);

            return new RequestConfigurator(relativeAddress, httpMethod);

        }

        /// <summary>
        /// Add a GET request to be intercepted
        /// </summary>
        /// <param name="host">Configuration handler for proxy server</param>
        /// <param name="relativeAddress">Relative uri of the request</param>
        /// <returns>Configuration handler</returns>
        public static IRequestHandler Get(this IResponseHandler host, string relativeAddress)
        {
            return Request(host, relativeAddress, "GET");
        }

        /// <summary>
        /// Add a POST request to be intercepted at the given address
        /// </summary>
        /// <param name="host">Configuration handler for proxy server</param>
        /// <param name="relativeAddress">Relative uri of the request</param>
        /// <returns>Configuration handler</returns>
        public static IRequestHandler Post(this IResponseHandler host, string relativeAddress)
        {
            return Request(host, relativeAddress, "POST");
        }

        /// <summary>
        /// Add a PUT request to be intercepted at the given address
        /// </summary>
        /// <param name="target">Configuration handler for proxy server</param>
        /// <param name="relativeAddress">Relative uri of the request</param>
        /// <returns>Configuration handler</returns>
        public static IRequestHandler Put(this IResponseHandler target, string relativeAddress)
        {
            return Request(target, relativeAddress, "PUT");
        }

        /// <summary>
        /// Add a DELETE request to be intercepted at the given address
        /// </summary>
        /// <param name="target">Configuration handler for proxy server</param>
        /// <param name="relativeAddress">Relative uri of the request</param>
        /// <returns>Configuration handler</returns>
        public static IRequestHandler Delete(this IResponseHandler target, string relativeAddress)
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
        /// <seealso>
        ///     <cref>Get</cref>
        /// </seealso>
        /// <seealso>
        ///     <cref>Put</cref>
        /// </seealso>
        /// <seealso>
        ///     <cref>Post</cref>
        /// </seealso>
        /// <seealso>
        ///     <cref>Delete</cref>
        /// </seealso>
        public static IRequestHandler Request(this IResponseHandler host, string relativeAddress, string httpMethod)
        {
           // CollectEvents(host);

            return new RequestConfigurator(relativeAddress, httpMethod);

        }

        /// <summary>
        /// Set the response to return on the previously added request address
        /// </summary>
        /// <param name="host">Configuration handler for proxy server</param>
        /// <param name="body">The required response body</param>
        /// <param name="statusCode">The required status code for the response</param>
        /// <returns>Configuration handler</returns>
        public static IResponseHandler Returns(this IRequestHandler host, string body, int statusCode)
        {
            return new ResponseConfigurator(body, statusCode);
        }

        /// <summary>
        /// Set the response to return on the previously added request address
        /// </summary>
        /// <param name="host">Configuration handler for proxy server</param>
        /// <param name="body">The required response body</param>
        /// <param name="statusCode">The required status code for the response</param>
        /// <param name="headers">Headers to set in response</param>
        /// <returns>Configuration handler</returns>
        public static IResponseHandler Returns(this IRequestHandler host, string body, int statusCode, IDictionary<string, string> headers)
        {
            return new ResponseConfigurator(body, statusCode, headers);
        }

        /// <summary>
        /// Returns all requests that were received by the proxy server
        /// </summary>
        /// <param name="target">Configuration handler for proxy server</param>
        /// <returns>List of requests</returns>
        /// <seealso cref="Rainbow.Testing.Boomerang.Host.Request"/>
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