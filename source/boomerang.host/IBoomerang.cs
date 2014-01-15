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
    }
}