namespace Rainbow.Testing.Boomerang.Host
{
    using System;

    /// <summary>
    /// Interface for proxy servers
    /// </summary>
    public interface IMasqarade
    {
        /// <summary>
        /// Fires when a request is received and before it is processed. />
        /// <remarks>
        /// The Masqarader raises this event with the address and method to inform Boomerang of the request <see cref="SetResponse"/>
        ///  </remarks>
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
        /// Boomerang uses this to tell the Masqarader the required response for the received request. <see cref="BeforeRequest"/>
        /// </summary>
        /// <param name="response">The response to set</param>
        void SetResponse(Response response);

        void Start(string address);
    }
}