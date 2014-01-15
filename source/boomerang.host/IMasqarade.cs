namespace Rainbow.Testing.Boomerang.Host
{
    using System;

    /// <summary>
    /// Interface for proxy servers
    /// </summary>
    public interface IMasqarade
    {
        /// <summary>
        /// Fires when a request is received. Used to intercept requests and set the response using <see cref="SetResponse"/>
        /// </summary>
        event EventHandler<ProxyRequestEventArgs> BeforeRequest;

        /// <summary>
        /// Starts the proxy server
        /// </summary>
        /// <param name="portNumber">Port number the proxy server should be listening on</param>
        void Start(int portNumber);

        /// <summary>
        /// Stops the proxy server
        /// </summary>
        void Stop();

        /// <summary>
        /// Set the response the proxy server should return for a request
        /// </summary>
        /// <param name="response">The response to set</param>
        void SetResponse(Response response);
    }
}