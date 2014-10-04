using System.Collections.Generic;

namespace Rainbow.Testing.Boomerang.Host
{
    /// <summary>
    /// Stores requests and resposnes as a list.  It only stores one response for each url and http verb combination.
    /// If more than one response is configured for a url and verb, the most recently configured response will always be used.
    /// </summary>
    internal class SingleResponseRepository : IResponseRepository
    {
        public IDictionary<Request, Response> RegisteredAddresses = new Dictionary<Request, Response>();
        private Request previouseRequest;

        public IEnumerable<Queue<Response>> Requests()
        {
            throw new System.NotImplementedException();
        }

        public void AddAddress(Request request)
        {
            previouseRequest = request;

            if (!RegisteredAddresses.ContainsKey(request))
            {
                RegisteredAddresses.Add(new KeyValuePair<Request, Response>(request, null));
            }
        }

        public void AddResponse(string body, int statusCode)
        {
            if (RegisteredAddresses.ContainsKey(previouseRequest))
            {
                Response response = null;
                RegisteredAddresses.TryGetValue(previouseRequest, out response);
                RegisteredAddresses.Remove(new KeyValuePair<Request, Response>(previouseRequest, response));
                RegisteredAddresses.Add(new KeyValuePair<Request, Response>(previouseRequest, new Response() { Body = body, StatusCode = statusCode }));
            }
        }

        public void AddResponse(string body, int statusCode, IDictionary<string, string> headers)
        {
            if (RegisteredAddresses.ContainsKey(previouseRequest))
            {
                Response response = null;
                RegisteredAddresses.TryGetValue(previouseRequest, out response);
                RegisteredAddresses.Remove(new KeyValuePair<Request, Response>(previouseRequest, response));
                RegisteredAddresses.Add(new KeyValuePair<Request, Response>(previouseRequest, new Response() { Body = body, StatusCode = statusCode, Headers = headers }));
            }
        }

        public bool Contains(Request request)
        {
            return RegisteredAddresses.ContainsKey(request);
        }

        public int GetCount()
        {
            return RegisteredAddresses.Count;
        }

        public bool GetAllResponsesFor(Request request, out Queue<Response> req)
        {
            throw new System.NotImplementedException();
        }

        public Response GetNextResponseFor(string method, string addressTarget)
        {
            Response response=null;
            RegisteredAddresses.TryGetValue(new Request() {Address = addressTarget, Method = method}, out response);

            return response;
        }
    }
}