namespace Rainbow.Testing.Boomerang.Host
{
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public interface IRequestResponses
    {
        IEnumerable<Queue<Response>> Requests();

        void AddAddress(Request request);

        void AddResponse(string body, int statusCode);

        bool Contains(Request request);

        int GetCount();

        bool GetAllResponsesFor(Request request, out  Queue<Response> req);

        Response GetNextResponseFor(string method, string addressTarget);

        void AddResponse(string body, int statusCode, IDictionary<string, string> headers);
    }
}