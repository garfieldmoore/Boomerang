namespace Rainbow.Testing.Boomerang.Host
{
    using System;

    public static class UniformInterfaceExtensions
    {
        public static ISendBy Get(this IBoomerang host, string prefix)
        {
            host.AddAddress(prefix);
            return host as ISendBy;
        }
    }
}