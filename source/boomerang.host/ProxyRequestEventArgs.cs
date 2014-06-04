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

        /// <summary>
        /// The message body of the recieved request
        /// <remarks>For HTTP GETs this will be null. For HTTP POSTS and PUTS this will be the data in the request body.</remarks>
        /// </summary>
        public object Body   { get; set; }
    }
}