﻿namespace Rainbow.Testing.Boomerang.Host
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
            ((BoomarangImpl)server).Start("localhost", listeningOnPort);

            return server;
        }
    }
}