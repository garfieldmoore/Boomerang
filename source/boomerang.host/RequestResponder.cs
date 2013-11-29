﻿namespace Rainbow.Testing.Boomerang.Host
{
    using System.Collections.Generic;


    public class RequestResponder
    {
        public IDictionary<Registration, RegisteredResponses> RequestResponseRegistrations;

        private Registration previousRegistration;

        public static string ResourceNotFoundMessage = "Resource not found";

        public RequestResponder()
        {
            RequestResponseRegistrations = new Dictionary<Registration, RegisteredResponses>();
        }

        public Response GetResponse(string method, string addressTarget)
        {
            RegisteredResponses requestResponse;

            if (!addressTarget.StartsWith("/"))
            {
                addressTarget = "/" + addressTarget;
            }

            var foundRequest =
                RequestResponseRegistrations.TryGetValue(
                    new Registration() { Address = addressTarget, Method = method }, out requestResponse);

            if (!foundRequest)
            {
                return new Response() { StatusCode = 400, Body = ResourceNotFoundMessage };
            }

            var registeredResponse = requestResponse.Responses.Dequeue();

            return new Response() { StatusCode = registeredResponse.StatusCode, Body=registeredResponse.Body };
        }

        public void AddAddress(Registration request)
        {
            if (!request.Address.StartsWith("/"))
            {
                request.Address = "/" + request.Address;
            }

            var newRegistration = new Registration() { Method = request.Method, Address = request.Address };
            if (!RequestResponseRegistrations.ContainsKey(newRegistration))
            {
                this.previousRegistration = newRegistration;
                RequestResponseRegistrations.Add(new KeyValuePair<Registration, RegisteredResponses>(this.previousRegistration, new RegisteredResponses()));
            }
        }

        public void AddResponse(string body, int statusCode)
        {
            RequestResponseRegistrations[previousRegistration].Responses.Enqueue(new Response() { Body = body, StatusCode = statusCode });
        }
    }
}