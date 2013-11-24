namespace Rainbow.Testing.Boomerang.Host
{
    public class Boomerang
    {
        private static Boomarang server;

        public static IBoomerang Server(int listeningOnPort)
        {
            if (server != null)
            {
                return server;
            }

            server = new Boomarang();
            server.Start("localhost", listeningOnPort);

            return server;
        }
    }
}