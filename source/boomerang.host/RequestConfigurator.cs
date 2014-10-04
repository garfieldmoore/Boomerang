namespace Rainbow.Testing.Boomerang.Host
{
    /// <summary>
    /// Handles requests for a http verb at a url
    /// </summary>
    internal class RequestConfigurator : IRequestHandler
    {
        public RequestConfigurator(string relativeAddress, string httpMethod)
        {
            var requestResponse = new Request { Address = relativeAddress, Method = httpMethod };
            RequestHandlers.Handler.AddAddress(requestResponse);
        }
    }
}