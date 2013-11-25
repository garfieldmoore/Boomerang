namespace Rainbow.Testing.Boomerang.Host
{
    using System.Threading;

    public class Boomerang
    {
        private static IBoomerang server;

        private static IBoomerangConfigurationFactory configurationFactory;

        static Boomerang()
        {
            configurationFactory = new BoomerangConfigurationFactory();
        }

        public static IBoomerang Server(int listeningOnPort)
        {
            if (server != null)
            {
                return server;
            }

            server = configurationFactory.Create();
            server.Start("localhost", listeningOnPort);

            return server;
        }

        public static void Initialize(IBoomerangConfigurationFactory boomerangConfigurationFactory)
        {
            configurationFactory = boomerangConfigurationFactory;
        }
    }
}