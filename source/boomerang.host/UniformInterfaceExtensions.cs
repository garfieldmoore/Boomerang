
namespace Rainbow.Testing.Boomerang.Host
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// Helper methods to configure requests to intercept and desired responses from proxy server
    /// </summary>
    public static class UniformInterfaceExtensions
    {
        private static Dictionary<IBoomerang, IList<Request>> proxyReferences = new Dictionary<IBoomerang, IList<Request>>();

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
        /// <param name="host">Configuration handler for proxy server</param>
        /// <param name="relativeAddress">Relative uri of the request</param>
        /// <returns>Configuration handler</returns>
        public static IRequestHandler Put(this IBoomerang host, string relativeAddress)
        {
            return Request(host, relativeAddress, "PUT");
        }

        /// <summary>
        /// Add a DELETE request to be intercepted at the given address
        /// </summary>
        /// <param name="host">Configuration handler for proxy server</param>
        /// <param name="relativeAddress">Relative uri of the request</param>
        /// <returns>Configuration handler</returns>
        public static IRequestHandler Delete(this IBoomerang host, string relativeAddress)
        {
            return Request(host, relativeAddress, "DELETE");
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
        /// <param name="host">Configuration handler for proxy server</param>
        /// <param name="relativeAddress">Relative uri of the request</param>
        /// <returns>Configuration handler</returns>
        public static IRequestHandler Put(this IResponseHandler host, string relativeAddress)
        {
            return Request(host, relativeAddress, "PUT");
        }

        /// <summary>
        /// Add a DELETE request to be intercepted at the given address
        /// </summary>
        /// <param name="host">Configuration handler for proxy server</param>
        /// <param name="relativeAddress">Relative uri of the request</param>
        /// <returns>Configuration handler</returns>
        public static IRequestHandler Delete(this IResponseHandler host, string relativeAddress)
        {
            return Request(host, relativeAddress, "DELETE");
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
        /// <param name="host">Configuration handler for proxy server</param>
        /// <returns>List of requests</returns>
        /// <seealso cref="Rainbow.Testing.Boomerang.Host.Request"/>
        public static IEnumerable<Request> GetAllReceivedRequests(this IBoomerang host)
        {
            return GetRequestsFor(host);
        }

        /// <summary>
        /// Clear the requests collected for an instance
        /// </summary>
        [Obsolete("No need for this as the events are collected for each instance. Simply start a new instance")]
        public static void ClearReceivedRequests(this IBoomerang host)
        {
            var requests = GetRequestsFor(host);
            requests.Clear();
        }

        private static void CollectEvents(IBoomerang host)
        {
            AttachEventTracker(host);
        }

        private static void AttachEventTracker(IBoomerang host)
        {
            if (!proxyReferences.ContainsKey(host))
            {
                proxyReferences.Add(host, new List<Request>());
            }

            EnsureTrackingEventsOnce(host);
        }

        private static void EnsureTrackingEventsOnce(IBoomerang host)
        {
            host.OnReceivedRequest -= host_OnReceivedRequest;
            host.OnReceivedRequest += host_OnReceivedRequest;
        }

        private static void host_OnReceivedRequest(object sender, ProxyRequestEventArgs e)
        {
            var host = sender as IBoomerang;
            if (host == null)
            {
                return;
            }

            IList<Request> requests = TryGetRequestsFor(host);
            Debug.Assert(requests!=null, "Have not attached event handler");

            if (FoundRequests(requests))
            {
                requests.Add(CreateRequestFromEvent(e));
            }
        }

        private static bool FoundRequests(IList<Request> requests)
        {
            return null != requests;
        }

        private static Request CreateRequestFromEvent(ProxyRequestEventArgs e)
        {
            return new Request() { Method = e.Method, Address = e.RelativePath, Body = e.Body };
        }

        private static IList<Request> GetRequestsFor(IBoomerang target)
        {
            IList<Request> requests;
            var hasRequests = proxyReferences.TryGetValue(target, out requests);
            if (hasRequests)
            {
                return requests;
            }

            return new List<Request>();
        }

        private static IList<Request> TryGetRequestsFor(IBoomerang host)
        {
            IList<Request> requests;
            proxyReferences.TryGetValue(host, out requests);

            return requests;
        }
    }
}