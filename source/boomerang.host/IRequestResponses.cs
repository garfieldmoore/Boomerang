namespace Rainbow.Testing.Boomerang.Host
{
    using System.Collections.Generic;

    /// <summary>
    /// Handles registering responses to requests
    /// </summary>
    public interface IRequestResponses
    {
        /// <summary>
        /// Returns the responses registered for all addresses
        /// </summary>
        /// <returns>Returns all responses for all address</returns>
        IEnumerable<Queue<Response>> Requests();

        /// <summary>
        /// Registers a HTTP request to intercept
        /// </summary>
        /// <param name="request">The method and relative uri to intercept</param>
        void AddAddress(Request request);

        /// <summary>
        /// Add response to the last address added
        /// </summary>
        /// <param name="body">the response body</param>
        /// <param name="statusCode">the response status code</param>
        void AddResponse(string body, int statusCode);

        /// <summary>
        /// Determines if a request has been registered
        /// </summary>
        /// <param name="request">The request to check</param>
        /// <returns>True if the method and uri has been configured, false otherwise</returns>
        bool Contains(Request request);

        /// <summary>
        /// Returns the number of registered uri's
        /// </summary>
        /// <returns>Number of registered uri's</returns>
        int GetCount();

        /// <summary>
        /// Return all responses for a request
        /// </summary>
        /// <param name="request">The request to return responses for</param>
        /// <param name="req">Set this to the configured responses if they exist for the request, otherwise null</param>
        /// <returns>True if there were responses for the request</returns>
        bool GetAllResponsesFor(Request request, out Queue<Response> req);

        /// <summary>
        /// Returns the next Response for a Request
        /// </summary>
        /// <param name="method">The HTTP request method</param>
        /// <param name="addressTarget">The relative uri we want a response for</param>
        /// <returns>The next response if there is one registered, otherwise a HTTP Resource Not Found (400) response</returns>
        Response GetNextResponseFor(string method, string addressTarget);

        /// <summary>
        /// Add response to the last address added
        /// </summary>
        /// <param name="body">the response body</param>
        /// <param name="statusCode">the response status code</param>
        /// <param name="headers">The headers expected in the response. If setting this the default headers will not be set.</param>
        void AddResponse(string body, int statusCode, IDictionary<string, string> headers);
    }
}