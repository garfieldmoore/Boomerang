namespace Rainbow.Testing.Boomerang.Host
{
    using System;

    /// <summary>
    /// Event args raised by proxy when a request is made.
    /// </summary>
    public class ProxyRequestEventArgs : EventArgs
    {
        public string Method { get; set; }

        public string RelativePath { get; set; }
    }
}