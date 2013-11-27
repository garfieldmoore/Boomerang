namespace Rainbow.Testing.Boomerang.Host
{
    using System;

    public static class UniformInterfaceExtensions
    {
        public static IBoomerang Get(this IBoomerang host, string prefix)
        {
            var requestResponse = new RequestResponse { Address = prefix, Method = "GET"};
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
            var requestResponse = new RequestResponse { Address = relativeAddress, Method = "POST"};
            ((BoomarangImpl)host).AddAddress(requestResponse);
            return host;
        }

        public static IBoomerang Returns(this IBoomerang host, int statusCode, string body)
        {
            ((BoomarangImpl)host).AddResponse(body, statusCode);
            return host;
        }
    }
}