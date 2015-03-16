namespace Rainbow.Testing.Boomerang.Host
{
    using System.Collections.Generic;

    /// <summary>
    /// Handles registering responses to requests
    /// </summary>
    public interface IResponseRepository
    {
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
        /// Add response to the last address added
        /// </summary>
        /// <param name="body">the response body</param>
        /// <param name="statusCode">the response status code</param>
        /// <param name="headers">The headers expected in the response. If setting this the default headers will not be set.</param>
        void AddResponse(string body, int statusCode, IDictionary<string, string> headers);

        /// <summary>
        /// Returns the next Response for a Request
        /// </summary>
        /// <param name="method">The HTTP request method</param>
        /// <param name="addressTarget">The relative uri we want a response for</param>
        /// <returns>The next response if there is one registered, otherwise a HTTP Resource Not Found (400) response</returns>
        Response GetNextResponseFor(string method, string addressTarget);
    }
}