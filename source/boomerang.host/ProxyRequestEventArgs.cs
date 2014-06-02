namespace Rainbow.Testing.Boomerang.Host
{
    using System;

    /// <summary>
    /// Event args raised by proxy when a request is made.
    /// </summary>
    public class ProxyRequestEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the HTTP method
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// Gets or sets the relative uri of the request
        /// </summary>
        public string RelativePath { get; set; }

        public object Body   { get; set; }
    }
}