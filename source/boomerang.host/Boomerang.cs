namespace Rainbow.Testing.Boomerang.Host
{
    using System;
    using System.ComponentModel;

    using Rainbow.Testing.Boomerang.Host.Configuration;
    using Rainbow.Testing.Boomerang.Host.HttpListenerProxy;

    /// <summary>
    /// Factory to create proxy servers
    /// </summary>
    public class Boomerang
    {
        private static IBoomerang server;
        private static IBoomerangConfigurationFactory hostFactory;
        private static object serverLock = new object();

        public static readonly int DefaultHttpPort = 5100;

        static Boomerang()
        {
            hostFactory = new DefaultConfigurationFactory();
        }

        /// <summary>
        /// Creates a new web service
        /// </summary>
        /// <param name="listeningOnPort">The port number to listen on.</param>
        /// <returns>Returns a proxy server listening on [port]</returns>
        /// <remarks>The server creates a single proxy.  Multiple calls will return the same proxy server</remarks>
        [Obsolete("Use the create factory method")]
        public static IBoomerang Server(int listeningOnPort)
        {
            EnsureBackwardCompatible();

            lock (serverLock)
            {
                if (server != null)
                {
                    return server;
                }

                server = new BoomarangImpl(hostFactory.Create());

                server.Start(listeningOnPort);
            }
            return server;
        }

        /// <summary>
        /// Creates a new web service listening
        /// </summary>
        /// <returns>Returns a proxy server listening on an available port</returns>
        /// <remarks>The server creates a single proxy.  Multiple calls will return the same proxy server</remarks>
        [Obsolete("Use the create method.")]
        public static IBoomerang Server()
        {
            return Server(DefaultHttpPort);
        }

        /// <summary>
        /// Overrides the default factory for Boomerang
        /// </summary>
        /// <param name="boomerangFactory">The factory used for creating Boomerang</param>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Use Create() method and set Host builder with the UseHostBuilder() method")]
        public static void Initialize(IBoomerangConfigurationFactory boomerangFactory)
        {
            lock (serverLock)
            {
                if (server != null)
                {
                    ((BoomarangImpl)server).Stop();
                }

                server = null;
                hostFactory = boomerangFactory;
            }
        }

        /// <summary>
        /// Create a new proxy server.
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static IBoomerang Create(Action<IHostConfiguration> configuration)
        {
            var hostConfigurator = new HostConfigurator();
            if (hostFactory != null)
                hostConfigurator.UseHostBuilder(()=>new HttpListenerFactory().Create());

            configuration(hostConfigurator);
            return hostConfigurator.CreateHost();
        }

        private static void EnsureBackwardCompatible()
        {
            if (RequestHandlers.Handler != null && RequestHandlers.Handler.GetType() != typeof(ResponseRepository))
            {
                RequestHandlers.Handler = new ResponseRepository();
            }
        }
    }
}