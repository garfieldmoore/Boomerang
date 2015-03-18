using System.Dynamic;

namespace Rainbow.Testing.Boomerang.Host
{
    using System;
    using Configuration;
    using HttpListenerProxy;

    /// <summary>
    /// Factory to create proxy servers
    /// </summary>
    public class Boomerang
    {
        private static IBoomerang server;
        private static IBoomerangConfigurationFactory hostFactory;

        public static readonly int DefaultHttpPort = 5100;

        static Boomerang()
        {
            hostFactory = new DefaultConfigurationFactory();
        }

        /// <summary>
        /// Create a new proxy server.
        /// </summary>
        /// <param name="configuration">Allows configuration of the host</param>
        /// <returns>Instance of the host</returns>
        /// <exception cref="OsVersionException">
        /// Thrown if working on an unsupported operating system. Use <see cref="IHostConfiguration.UseHostBuilder();"/> to inject an implementation.
        /// </exception>
        public static IBoomerang Create(Action<IHostConfiguration> configuration)
        {
            var hostConfigurator = new HostConfigurator();
            if (hostFactory != null)
                hostConfigurator.UseHostBuilder(()=>new HttpListenerFactory().Create());

            configuration(hostConfigurator);
            return hostConfigurator.CreateHost();
        }
    }
}