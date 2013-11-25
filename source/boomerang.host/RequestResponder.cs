namespace Rainbow.Testing.Boomerang.Host
{
    using System.Collections.Generic;

    using System.Linq;

    public class RequestResponder
    {
        public RequestResponse GetResponse(string method, string addressTarget, IList<RequestResponse> responses)
        {
            var respone = responses.FirstOrDefault(x => x.Address == addressTarget && x.Method == method);
            if (respone == null)
            {
                respone = new RequestResponse();
            }

            return respone;
        }

        public RequestResponse GetResponse(string addressTarget, IList<RequestResponse> requestResponses)
        {
            return GetResponse("GET", addressTarget, requestResponses);
        }
    }
}