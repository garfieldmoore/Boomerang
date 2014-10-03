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
     
        public void UseHostBuilder(IBoomerangConfigurationFactory hostFactory)
        {
            HostConfigurator.hostFactory = hostFactory;
        }

        public void AtAddress(string url)
        {
            settings.Prefixes.Add(url);
        }

        public void AlwaysRespondWithLastConfiguredResponse()
        {
            throw new System.NotImplementedException();
        }

        public IBoomerang CreateHost()
        {
            return new BoomarangImpl(hostFactory.Create(), settings);
        }
    }
}