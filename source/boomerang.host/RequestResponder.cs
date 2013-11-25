namespace Rainbow.Testing.Boomerang.Host
{
    using System.Collections.Generic;

    public class RequestResponder
    {
        public Response GetResponse(string addressTarget, IList<Response> responses, IList<string> address)
        {
            if (!address.Contains(addressTarget))
            {
                return Response.CreateNew();
            }

            if (address.IndexOf(addressTarget) >= responses.Count)
            {
                return Response.CreateNew();
            }

            return responses[address.IndexOf(addressTarget)];
        }
    }
}