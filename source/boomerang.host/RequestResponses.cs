namespace Rainbow.Testing.Boomerang.Host
{
    using System.Collections.Generic;

    /// <summary>
    /// Handles registering responses to requests
    /// </summary>
    public class RequestResponses : IRequestResponses
    {
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

        public IEnumerable<Queue<Response>> Requests()
        {
            return RequestResponseRegistrations.Values;
        }

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

        public void AddResponse(string body, int statusCode)
        {
            RequestResponseRegistrations[this.previousRequest].Enqueue(
                new Response() { Body = body, StatusCode = statusCode });
        }

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

        public bool Contains(Request request)
        {
            return RequestResponseRegistrations.ContainsKey(request);
        }

        public int GetCount()
        {
            return RequestResponseRegistrations.Count;
        }

        public bool GetAllResponsesFor(Request request, out Queue<Response> req)
        {
            return RequestResponseRegistrations.TryGetValue(request, out req);
        }

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