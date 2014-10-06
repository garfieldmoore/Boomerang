namespace Rainbow.Testing.Boomerang.Host.Configuration
{
    using System;

    using Rainbow.Testing.Boomerang.Host.HttpListenerProxy;

    public class HostConfigurator : IHostConfiguration
    {
        HostSettings settings;

        private Func<IResponseRepository> requestHandlerFactory;
        private Func<IMasqarade> hostFactoryFunc;


        public HostConfigurator()
        {
            settings = new HostSettings();
            requestHandlerFactory = () => new ResponseRepository();
        }

        public void UseHostBuilder(Func<IMasqarade> hostFactoryFunc)
        {
            this.hostFactoryFunc = hostFactoryFunc;
        }

        public void AtAddress(string url)
        {
            settings.Prefixes.Add(url);
        }

        public void UseSingleResponsePerRequestHandler()
        {
            requestHandlerFactory = () => new SingleResponseRepository();
        }

        public IBoomerang CreateHost()
        {
            return new BoomarangImpl(hostFactoryFunc(), settings, requestHandlerFactory);
        }
    }
}