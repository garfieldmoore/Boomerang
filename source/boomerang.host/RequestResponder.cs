namespace Rainbow.Testing.Boomerang.Host
{
    using System.Collections.Generic;


    public class RequestResponder
    {
        public IDictionary<Registration, RequestResponse> RequestResponseRegistrations;

        private Registration previousRegistration;

        public static string ResourceNotFoundMessage = "Resource not found";

        public RequestResponder()
        {
            RequestResponseRegistrations = new Dictionary<Registration, RequestResponse>();
        }

        public RequestResponse GetResponse(string method, string addressTarget)
        {
            RequestResponse responsed;

            if (!addressTarget.StartsWith("/"))
            {
                addressTarget = "/" + addressTarget;
            }

            var respone =
                RequestResponseRegistrations.TryGetValue(
                    new Registration() { Address = addressTarget, Method = method }, out responsed);

            if (!respone)
            {
                return new RequestResponse() { Response = new Response() { StatusCode = 400, Body = ResourceNotFoundMessage } };
            }

            var interim = new RequestResponse() { Response = responsed.Responses.Dequeue() };
            return interim;
        }

        public RequestResponse GetResponse(string addressTarget)
        {
            return GetResponse("GET", addressTarget);
        }


        public void AddAddress(RequestResponse request)
        {
            if (!request.Address.StartsWith("/"))
            {
                request.Address = "/" + request.Address;
            }

            var newRegistration = new Registration() { Method = request.Method, Address = request.Address };
            if (!RequestResponseRegistrations.ContainsKey(newRegistration))
            {
                this.previousRegistration = newRegistration;
                RequestResponseRegistrations.Add(new KeyValuePair<Registration, RequestResponse>(this.previousRegistration, new RequestResponse()));
            }
        }

        public void AddResponse(string body, int statusCode)
        {
            RequestResponseRegistrations[previousRegistration].Responses.Enqueue(new Response() { Body = body, StatusCode = statusCode });
        }

        public void AddResponse(string body, int statusCode, string responseBody)
        {
            RequestResponseRegistrations[previousRegistration].Responses.Enqueue(new Response() { Body = body, StatusCode = statusCode, ResponseDescription = responseBody });
        }
    }
}