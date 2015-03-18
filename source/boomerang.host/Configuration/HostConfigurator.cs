namespace Rainbow.Testing.Boomerang.Host.Configuration
{
    using System;

    /// <summary>
    /// Configures Boomerang
    /// </summary>
    public class HostConfigurator : IHostConfiguration
    {
        readonly HostSettings settings;

        private Func<IResponseRepository> requestHandlerFactory;
        private Func<IMasqarade> hostFactoryFunc;

        /// <summary>
        /// Default constructor
        /// </summary>
        public HostConfigurator()
        {
            settings = new HostSettings();
            requestHandlerFactory = () => new ResponseRepository();
        }

        /// <summary>
        /// Sets the http listener that listens for web requests
        /// </summary>
        /// <param name="hostbuilder">A function that creates an IMasquarade listener implementation</param>
        public void UseHostBuilder(Func<IMasqarade> hostbuilder)
        {
            this.hostFactoryFunc = hostbuilder;
        }

        /// <summary>
        /// Defines the address the proxy is listening at
        /// </summary>
        /// <param name="url">The url to listen for requests on</param>
        public void AtAddress(string url)
        {
            settings.Prefixes.Add(url);
        }

        /// <summary>
        /// Sets a factory used to create the request handler.
        /// </summary>
        /// <param name="responseRepositoryFactory"></param>
        public void UseRequestHandlerFactory(Func<IResponseRepository> responseRepositoryFactory)
        {
            requestHandlerFactory = responseRepositoryFactory;
        }

        /// <summary>
        /// Creates the host
        /// </summary>
        /// <returns>An instance of the Boomerang host that allows configuring of http requests</returns>
        public IBoomerang CreateHost()
        {
            return new BoomarangImpl(hostFactoryFunc(), settings, requestHandlerFactory);
        }
    }
}