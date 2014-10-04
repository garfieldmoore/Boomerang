using System.Collections.Generic;

namespace Rainbow.Testing.Boomerang.Host
{
    /// <summary>
    /// 
    /// </summary>
    internal class ResponseConfigurator : IResponseHandler
    {
        public ResponseConfigurator(string body, int statusCode, IDictionary<string, string> headers)
        {
            RequestHandlers.Handler.AddResponse(body, statusCode,headers);
        }

        public ResponseConfigurator(string body, int statusCode)
        {
            RequestHandlers.Handler.AddResponse(body, statusCode);
        }
    }
}