namespace Rainbow.Testing.Boomerang.Host
{
    /// <summary>
    /// Entry point to create proxy web services
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
        /// <returns>Returns a proxy server listening on http://localhost:[port]</returns>
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