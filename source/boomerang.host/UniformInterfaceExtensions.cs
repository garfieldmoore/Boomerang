namespace Rainbow.Testing.Boomerang.Host
{

    public static class UniformInterfaceExtensions
    {
        public static IBoomerang Get(this IBoomerang host, string prefix)
        {
            var requestResponse = new Registration { Address = prefix, Method = "GET" };
            ((BoomarangImpl)host).AddAddress(requestResponse);
            return host;
        }

        public static IBoomerang Returns(this IBoomerang host, string body, int statusCode)
        {
            ((BoomarangImpl)host).AddResponse(body, statusCode);
            return host;
        }

        public static IBoomerang Post(this IBoomerang host, string relativeAddress, string data)
        {
            host.Request(relativeAddress, data, "POST");
            var requestResponse = new Registration { Address = relativeAddress, Method = "POST" };
            ((BoomarangImpl)host).AddAddress(requestResponse);
            return host;
        }

        public static IBoomerang Request(this IBoomerang host, string relativeAddress, string data, string httpMethod)
        {
            var requestResponse = new Registration { Address = relativeAddress, Method = httpMethod };
            ((BoomarangImpl)host).AddAddress(requestResponse);
            return host;
        }

        public static IBoomerang Put(this IBoomerang target, string relativeAddress, string data)
        {
            var requestResponse = new Registration { Address = relativeAddress, Method = "PUT" };
            ((BoomarangImpl)target).AddAddress(requestResponse);
            return target;
        }

        public static IBoomerang Delete(this IBoomerang target, string relativeAddress)
        {
            var requestResponse = new Registration { Address = relativeAddress, Method = "DELETE" };
            ((BoomarangImpl)target).AddAddress(requestResponse);
            return target;
        }

    }
}