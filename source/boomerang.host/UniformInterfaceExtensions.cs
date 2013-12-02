namespace Rainbow.Testing.Boomerang.Host
{

    public static class UniformInterfaceExtensions
    {
        public static IBoomerang Get(this IBoomerang host, string prefix)
        {
            var requestResponse = new Request { Address = prefix, Method = "GET" };
            ((BoomarangImpl)host).AddAddress(requestResponse);
            return host;
        }

        public static IBoomerang Post(this IBoomerang host, string relativeAddress)
        {
            host.Request(relativeAddress, "POST");
            return host;
        }

        public static IBoomerang Put(this IBoomerang target, string relativeAddress)
        {
            target.Request(relativeAddress, "PUT");
            return target;
        }

        public static IBoomerang Delete(this IBoomerang target, string relativeAddress)
        {
            target.Request(relativeAddress, "DELETE");
            return target;
        }

        public static IBoomerang Request(this IBoomerang host, string relativeAddress, string httpMethod)
        {
            var requestResponse = new Request { Address = relativeAddress, Method = httpMethod };
            ((BoomarangImpl)host).AddAddress(requestResponse);
            return host;
        }
    
        public static IBoomerang Returns(this IBoomerang host, string body, int statusCode)
        {
            ((BoomarangImpl)host).AddResponse(body, statusCode);
            return host;
        }
    }
}