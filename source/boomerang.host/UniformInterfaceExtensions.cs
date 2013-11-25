namespace Rainbow.Testing.Boomerang.Host
{

    public static class UniformInterfaceExtensions
    {
        public static IBoomerang Get(this IBoomerang host, string prefix)
        {
            var requestResponse = new RequestResponse { Address = prefix, Method = "GET", Instance = 0 };
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