namespace Rainbow.Testing.Boomerang.Host
{
    /// <summary>
    /// Factory to create proxy servers
    /// </summary>
    public class Boomerang
    {
        private static IBoomerang server;

        private static IBoomerangConfigurationFactory configurationFactory;

        static Boomerang()
        {
            configurationFactory = new BoomerangConfigurationFactory();
        }

        /// <summary>
        /// Creates a new web service
        /// </summary>
        /// <param name="listeningOnPort">The port number to listen on.</param>
        /// <returns>Returns a proxy server listening on [port]</returns>
        /// <remarks>The server creates a single proxy.  Multiple calls will return the same proxy server</remarks>
        public static IBoomerang Server(int listeningOnPort)
        {
            if (server != null)
            {
                return server;
            }

            server = configurationFactory.Create();
            ((BoomarangImpl)server).Start(listeningOnPort);

            return server;
        }

        /// <summary>
        /// Creates a new web service
        /// </summary>
        /// <returns>Returns a proxy server listening on an available port</returns>
        /// <remarks>The server creates a single proxy.  Multiple calls will return the same proxy server</remarks>
        internal static IBoomerang Server()
        {
            // TODO not selecting port correctly
            return Server(0);
        }
    }
}