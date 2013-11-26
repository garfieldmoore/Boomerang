namespace Rainbow.Testing.Boomerang.Host
{
    using System.Collections.Generic;

    using System.Linq;

    public class RequestResponder
    {
        public IList<RequestResponse> RequestResponses;

        public RequestResponder()
        {
            RequestResponses = new List<RequestResponse>();
        }

        public RequestResponder(IList<RequestResponse> responses)
        {
            RequestResponses = responses;
        }

        public RequestResponse GetResponse(string method, string addressTarget)
        {
            var respone = RequestResponses.FirstOrDefault(x => x.Address == addressTarget && x.Method == method);
            if (respone == null)
            {
                respone = new RequestResponse();
            }

            return respone;
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

            RequestResponses.Add(request);
        }

        public void AddResponse(string body, int statusCode)
        {
            this.RequestResponses[RequestResponses.Count - 1].Response = new Response() { Body = body, StatusCode = statusCode };
        }
    }
}