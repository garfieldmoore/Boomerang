namespace Rainbow.Testing.Boomerang.Host.Configuration
{
    public class HostConfigurator : IHostConfiguration
    {
        private int startingPort;

        private static IBoomerangConfigurationFactory hostFactory;

        public HostConfigurator()
        {
            hostFactory = new DefaultConfigurationFactory();

        }

        public void OnPort(int portNumber)
        {
            startingPort = portNumber;
        }

        public void UseHostBuilder(IBoomerangConfigurationFactory hostFactory)
        {
            HostConfigurator.hostFactory = hostFactory;
        }

        public IBoomerang CreateHost()
        {
            return new BoomarangImpl(hostFactory.Create());
        }
    }
}