namespace Rainbow.Testing.Boomerang.Host
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Interface for proxy server controllers
    /// </summary>
    public interface IBoomerang
    {
        /// <summary>
        /// Fired when requests are received and before the request is processed
        /// </summary>
        event EventHandler<ProxyRequestEventArgs> OnReceivedRequest;

        BoomerangExitCode Start(int port);
        BoomerangExitCode Start();
    }

    public class HostSettings
    {
        public HostSettings()
        {
            Prefixes = new List<string>();
        }

        public IList<string> Prefixes { get; set; }
    }
}