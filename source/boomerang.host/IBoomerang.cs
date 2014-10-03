namespace Rainbow.Testing.Boomerang.Host
{
    using System;

    /// <summary>
    /// Interface for proxy server controllers
    /// </summary>
    public interface IBoomerang
    {
        /// <summary>
        /// Fired when requests are received and before the request is processed
        /// </summary>
        event EventHandler<ProxyRequestEventArgs> OnReceivedRequest;

        /// <summary>
        /// Start the proxy server
        /// </summary>
        /// <param name="port">The port number to listen on</param>
        [Obsolete("Configure with Boomerang.Create adn use parameterless Start() method")]
        BoomerangExitCode Start(int port);

        /// <summary>
        /// Starts the proxy server after that has been configured using <code>Boomerang.Create(Function);</code>
        /// </summary>
        /// <returns></returns>
        BoomerangExitCode Start();
    }
}