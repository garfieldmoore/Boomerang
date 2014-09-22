namespace Rainbow.Testing.Boomerang.Host.Configuration
{
    public class HostConfigurator : IHostConfiguration
    {
        HostSettings settings;

        public HostConfigurator()
        {
            settings=new HostSettings();
            hostFactory = new DefaultConfigurationFactory();
        }
        private static IBoomerangConfigurationFactory hostFactory;

        public void OnPort(int portNumber)
        {
            settings.Port = portNumber;
        }

        public void UseHostBuilder(IBoomerangConfigurationFactory hostFactory)
        {
            HostConfigurator.hostFactory = hostFactory;
        }

        public IBoomerang CreateHost()
        {
            return new BoomarangImpl(hostFactory.Create(), settings);
        }
    }
}