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
        /// Starts the proxy server after that has been configured using <code>Boomerang.Create(Function);</code>
        /// </summary>
        /// <returns></returns>
        BoomerangExitCode Start();

        /// <summary>
        /// Stops the listener and disposes of any resources
        /// </summary>
        void Stop();
    }
}