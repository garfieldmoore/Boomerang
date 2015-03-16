namespace Rainbow.Testing.Boomerang.Host.Configuration
{
    using System;

    /// <summary>
    /// Configuration for Hosts
    /// </summary>
    public interface IHostConfiguration
    {
        /// <summary>
        /// Sets the http listener that listens for web requests
        /// </summary>
        /// <param name="hostFactoryFunc"></param>
        void UseHostBuilder(Func<IMasqarade> hostFactoryFunc);

        /// <summary>
        /// Defines the address the proxy is listening at
        /// </summary>
        /// <param name="url">The url to listen to requests at</param>
        void AtAddress(string url);

        /// <summary>
        /// Sets a factory used to create the request handler.
        /// </summary>
        /// <param name="responseRepositoryFactory"></param>
        void UseRequestHandlerFactory(Func<IResponseRepository> responseRepositoryFactory);
    }
}