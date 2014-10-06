namespace Rainbow.Testing.Boomerang.Host
{
    using System.Collections.Generic;

    /// <summary>
    /// Handles registering responses to requests.
    /// </summary>
    internal class ResponseRepository : IResponseRepository
    {
        /// <summary>
        /// Response message for requests that have no configured response
        /// </summary>
        public static string ResourceNotFoundMessage =
            "Boomerang error: Resource not found or no response configured for request";

        public IDictionary<Request, Queue<Response>> RequestResponseRegistrations;

        private Request previousRequest;

        /// <summary>
        /// Default constructor
        /// </summary>
        public ResponseRepository()
        {
            RequestResponseRegistrations = new Dictionary<Request, Queue<Response>>();
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

            var newRegistration = new Request() {Method = request.Method, Address = request.Address};
            previousRequest = newRegistration;
            if (!RequestResponseRegistrations.ContainsKey(newRegistration))
            {
                RequestResponseRegistrations.Add(
                    new KeyValuePair<Request, Queue<Response>>(previousRequest, new Queue<Response>()));
            }
        }

        /// <summary>
        /// Add response to the last address added
        /// </summary>
        /// <param name="body">the response body</param>
        /// <param name="statusCode">the response status code</param>
        public void AddResponse(string body, int statusCode)
        {
            RequestResponseRegistrations[previousRequest].Enqueue(
                new Response() {Body = body, StatusCode = statusCode});
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
            RequestResponseRegistrations[previousRequest].Enqueue(response);
        }

        /// <summary>
        /// Returns the next Response for a Request
        /// </summary>
        /// <param name="method">The HTTP request method</param>
        /// <param name="addressTarget">The relative uri we want a response for</param>
        /// <returns>The next response if there is one registered, otherwise a HTTP Resource Not Found (400) response</returns>
        public Response GetNextResponseFor(string method, string addressTarget)
        {
            addressTarget = ConvertToRelativeAddressFormat(addressTarget);

            var requestResponses = GetResponsesFor(CreateNewRequest(method, addressTarget));

            if (HasResponses(requestResponses))
            {
                return requestResponses.Dequeue();
            }

            return CreateNotFoundResponse();
        }

        private static bool HasResponses(Queue<Response> requestResponses)
        {
            return requestResponses != null && requestResponses.Count > 0;
        }

        private Queue<Response> GetResponsesFor(Request request)
        {
            Queue<Response> requestResponse;

            var foundRequest =
                RequestResponseRegistrations.TryGetValue(request, out requestResponse);

            return foundRequest ? requestResponse : null;
        }

        private static Request CreateNewRequest(string method, string addressTarget)
        {
            return new Request() {Address = addressTarget, Method = method};
        }

        private static Response CreateNotFoundResponse()
        {
            return new Response() {StatusCode = 400, Body = ResourceNotFoundMessage};
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