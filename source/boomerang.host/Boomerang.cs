namespace Rainbow.Testing.Boomerang.Host
{
    /// <summary>
    /// Factory to create proxy servers
    /// </summary>
    /// <example>
    /// Boomerang.Server(5100);
    /// </example>
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
        /// <returns>Returns a proxy server listening on http://localhost:[port]</returns>
        /// <remarks>The server creates a single proxy.  Multiple calls will return the same proxy server</remarks>
        public static IBoomerang Server(int listeningOnPort)
        {
            if (server != null)
            {
                return server;
            }

            server = configurationFactory.Create();
            ((BoomarangImpl)server).Start("localhost", listeningOnPort);

            return server;
        }
    }
}