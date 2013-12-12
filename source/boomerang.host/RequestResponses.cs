namespace Rainbow.Testing.Boomerang.Host
{
    using System.Collections.Generic;

    /// <summary>
    /// Handles registering responses to requests
    /// </summary>
    public class RequestResponses : IRequestResponses
    {
        /// <summary>
        /// Response message for requests that have no configured response
        /// </summary>
        public static string ResourceNotFoundMessage = "Boomerang error: Resource not found or no response configured for request";

        protected IDictionary<Request, Queue<Response>> RequestResponseRegistrations;

        private Request previousRequest;

        /// <summary>
        /// Default constructor
        /// </summary>
        public RequestResponses()
        {
            RequestResponseRegistrations = new Dictionary<Request, Queue<Response>>();
        }

        /// <summary>
        /// Returns the responses registered for all addresses
        /// </summary>
        /// <returns>Returns all responses for all address</returns>
        public IEnumerable<Queue<Response>> Requests()
        {
            return RequestResponseRegistrations.Values;
        }

        /// <summary>
        /// Registers a HTTP request to intercept
        /// </summary>
        /// <param name="request">The method and relative uri to intercept</param>
        public void AddAddress(Request request)
        {
            if (!request.Address.StartsWith("/"))
            {
                request.Address = "/" + request.Address;
            }

            var newRegistration = new Request() { Method = request.Method, Address = request.Address };
            if (!RequestResponseRegistrations.ContainsKey(newRegistration))
            {
                this.previousRequest = newRegistration;
                RequestResponseRegistrations.Add(
                    new KeyValuePair<Request, Queue<Response>>(this.previousRequest, new Queue<Response>()));
            }
        }

        /// <summary>
        /// Add response to the last address added
        /// </summary>
        /// <param name="body">the response body</param>
        /// <param name="statusCode">the response status code</param>
        public void AddResponse(string body, int statusCode)
        {
            RequestResponseRegistrations[this.previousRequest].Enqueue(
                new Response() { Body = body, StatusCode = statusCode });
        }

        /// <summary>
        /// Add response to the last address added
        /// </summary>
        /// <param name="body">the response body</param>
        /// <param name="statusCode">the response status code</param>
        /// <param name="headers">The headers expected in the response. If setting this the default headers will not be set.</param>
        public void AddResponse(string body, int statusCode, IDictionary<string, string> headers)
        {
            var response = new Response()
                               {
                                   Body = body,
                                   StatusCode = statusCode,
                                   Headers = new Dictionary<string, string>(headers)
                               };
            RequestResponseRegistrations[this.previousRequest].Enqueue(response);
        }

        /// <summary>
        /// Determines if a request has been registered
        /// </summary>
        /// <param name="request">The request to check</param>
        /// <returns>True if the method and uri has been configured, false otherwise</returns>
        public bool Contains(Request request)
        {
            return RequestResponseRegistrations.ContainsKey(request);
        }

        /// <summary>
        /// Returns the number of registered uri's
        /// </summary>
        /// <returns>Number of registered uri's</returns>
        public int GetCount()
        {
            return RequestResponseRegistrations.Count;
        }

        /// <summary>
        /// Return all responses for a request
        /// </summary>
        /// <param name="request">The request to return responses for</param>
        /// <param name="req">Set this to the configured responses if they exist for the request, otherwise null</param>
        /// <returns>True if there were responses for the request</returns>
        public bool GetAllResponsesFor(Request request, out Queue<Response> req)
        {
            return RequestResponseRegistrations.TryGetValue(request, out req);
        }

        /// <summary>
        /// Returns the next Response for a Request
        /// </summary>
        /// <param name="method">The HTTP request method</param>
        /// <param name="addressTarget">The relative uri we want a response for</param>
        /// <returns>The next response if there is one registered, otherwise a HTTP Resource Not Found (400) response</returns>
        public Response GetNextResponseFor(string method, string addressTarget)
        {
            Queue<Response> requestResponse;

            addressTarget = ConvertToRelativeAddressFormat(addressTarget);

            var foundRequest =
                RequestResponseRegistrations.TryGetValue(
                    new Request() { Address = addressTarget, Method = method }, out requestResponse);

            if (!foundRequest || requestResponse == null || requestResponse.Count == 0)
            {
                return new Response() { StatusCode = 400, Body = ResourceNotFoundMessage };
            }

            return requestResponse.Dequeue();
        }

        private static string ConvertToRelativeAddressFormat(string addressTarget)
        {
            if (!addressTarget.StartsWith("/"))
            {
                addressTarget = "/" + addressTarget;
            }

            return addressTarget;
        }
    }
}