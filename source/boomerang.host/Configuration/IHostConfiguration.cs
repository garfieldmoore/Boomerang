namespace Rainbow.Testing.Boomerang.Host.Configuration
{
    using System;

    /// <summary>
    /// Configuration for Hosts
    /// </summary>
    public interface IHostConfiguration
    {
        /// <summary>
        /// Provides a function to create the proxy server implementation
        /// </summary>
        /// <param name="hostFactory">The function that returns an IMasquarade object</param>
        /// <see cref="IMasqarade"/>
        void UseHostBuilder(Func<IMasqarade> hostFactory);

        /// <summary>
        /// The url to start the server at
        /// </summary>
        /// <param name="url">The URL.</param>
        void AtAddress(string url);

        /// <summary>
        /// Uses the single response per request handler.
        /// <remarks>
        /// By default Boomerang allows registeration of multiple responses for requests and returns a 404 when no more responses are available.
        /// Using this changes the behaviour to always returning one request.  if more than one request is configured, the last one will be used.
        /// </remarks>
        /// </summary>
        void UseSingleResponsePerRequestHandler();
    }
}