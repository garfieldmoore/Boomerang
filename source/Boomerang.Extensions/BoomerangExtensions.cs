using System;
using Rainbow.Testing.Boomerang.Host;

namespace Rainbow.Testing.Boomerang.Extensions
{
    public static class BoomerangExtensions
    {
        /// <summary>
        /// Configure actions n times
        /// </summary>
        /// <param name="host">Boomerang instance</param>
        /// <param name="configuration">The configuration as a lamda funciton</param>
        /// <param name="n">The number of times to repeat the configuration</param>
        /// <returns>The host instance</returns>
        /// <example>The below code configures 10 GET responses for http://localhost:5100/test
        /// <code>Boomerang.Server(5100).Repeat(x => x.Get("test").Returns("response"), 10)</code></example>
        public static IBoomerang Repeat(this IBoomerang host, Action<IBoomerang> configuration, int n)
        {
            for (int i = 0; i < n; i++)
            {
                configuration(host);
            }

            return host;
        }
    }
}
