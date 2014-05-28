using System;
using Rainbow.Testing.Boomerang.Host;

namespace Boomerang.Extensions
{
    public static class BoomerangExtensions
    {
        public static IBoomerang Repeat(this IBoomerang host, Action<IBoomerang> func, int n)
        {
            for (int i = 0; i < n; i++)
            {
                func(host);
            }

            return host;
        }
    }
}
