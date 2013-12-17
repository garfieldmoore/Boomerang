namespace Rainbow.Testing.Boomerang.Host
{
    using System.ComponentModel;
    using System.Threading;

    /// <summary>
    /// Factory to create proxy servers
    /// </summary>
    public class Boomerang
    {
        private static IBoomerang server;

        private static IBoomerangConfigurationFactory configurationFactory;

        private static object serverLock = new object();

        static Boomerang()
        {
            configurationFactory = new DefaultConfigurationFactory();
        }

        /// <summary>
        /// Creates a new web service
        /// </summary>
        /// <param name="listeningOnPort">The port number to listen on.</param>
        /// <returns>Returns a proxy server listening on [port]</returns>
        /// <remarks>The server creates a single proxy.  Multiple calls will return the same proxy server</remarks>
        public static IBoomerang Server(int listeningOnPort)
        {
            lock (serverLock)
            {
                if (server != null)
                {
                    return server;
                }

                server = configurationFactory.Create();
                ((BoomarangImpl)server).Start(listeningOnPort);                
            }

            return server;
        }

        /// <summary>
        /// Creates a new web service listening
        /// </summary>
        /// <returns>Returns a proxy server listening on an available port</returns>
        /// <remarks>The server creates a single proxy.  Multiple calls will return the same proxy server</remarks>
        public static IBoomerang Server()
        {
            return Server(0);
        }

        /// <summary>
        /// Overrides the default factory for Boomerang
        /// </summary>
        /// <param name="boomerangFactory">The factory used for creating Boomerang</param>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void Initialize(IBoomerangConfigurationFactory boomerangFactory)
        {
            lock (serverLock)
            {
                if (server!=null)
                    ((BoomarangImpl)server).Stop();

                server = null;
                configurationFactory = boomerangFactory;
            }
        }
    }
}