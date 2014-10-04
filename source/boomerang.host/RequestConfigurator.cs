namespace Rainbow.Testing.Boomerang.Host
{
    /// <summary>
    /// 
    /// </summary>
    public class RequestConfigurator : IRequestHandler
    {
        public RequestConfigurator(string relativeAddress, string httpMethod)
        {
            var requestResponse = new Request { Address = relativeAddress, Method = httpMethod };
            RequestHandlers.Handler.AddAddress(requestResponse);
        }
    }
}