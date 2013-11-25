namespace Rainbow.Testing.Boomerang.Host
{
    using System.Collections.Generic;

    public class RequestResponder
    {
        public Response GetResponse(string addressTarget, IList<Response> responses, IList<string> address)
        {
            if (!address.Contains(addressTarget))
            {
                return new Response();
            }

            if (address.IndexOf(addressTarget) >= responses.Count)
            {
                return new Response();
            }

            return responses[address.IndexOf(addressTarget)];
        }
    }
}