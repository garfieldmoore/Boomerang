namespace Rainbow.Testing.Boomerang.Host
{
    using System.Collections.Generic;

    public interface IRequestResponses
    {
        IEnumerable<Queue<Response>> Requests();

        void AddAddress(Request request);

        void AddResponse(string body, int statusCode);

        bool Contains(Request request);

        int GetCount();

        bool GetAllResponsesFor(Request request, out  Queue<Response> req);

        Response GetNextResponseFor(string method, string addressTarget);
    }

    public class RequestResponses : IRequestResponses
    {
        protected IDictionary<Request, Queue<Response>> RequestResponseRegistrations;

        private Request previousRequest;

        public static string ResourceNotFoundMessage = "Resource not found";

        public RequestResponses()
        {
            RequestResponseRegistrations = new Dictionary<Request, Queue<Response>>();
        }

        public IEnumerable<Queue<Response>> Requests()
        {
            return RequestResponseRegistrations.Values;
        }

        private static string ConvertToRelativeAddressFormat(string addressTarget)
        {
            if (!addressTarget.StartsWith("/"))
            {
                addressTarget = "/" + addressTarget;
            }
            return addressTarget;
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
                RequestResponseRegistrations.Add(new KeyValuePair<Request, Queue<Response>>(this.previousRequest, new Queue<Response>()));
            }
        }

        public void AddResponse(string body, int statusCode)
        {
            RequestResponseRegistrations[this.previousRequest].Enqueue(new Response() { Body = body, StatusCode = statusCode });
        }

        public bool Contains(Request request)
        {
            return RequestResponseRegistrations.ContainsKey(request);
        }

        public int GetCount()
        {
            return RequestResponseRegistrations.Count;
        }

        public bool GetAllResponsesFor(Request request, out  Queue<Response> req)
        {
            return RequestResponseRegistrations.TryGetValue(request, out req);
        }

        public Response GetNextResponseFor(string method, string addressTarget)
        {
            Queue<Response> requestResponse;

            addressTarget = ConvertToRelativeAddressFormat(addressTarget);

            var foundRequest = RequestResponseRegistrations.TryGetValue(new Request() { Address = addressTarget, Method = method }, out requestResponse);

            if (!foundRequest)
            {
                return new Response() { StatusCode = 400, Body = ResourceNotFoundMessage };
            }
            
            return requestResponse.Dequeue();
        }
    }
}