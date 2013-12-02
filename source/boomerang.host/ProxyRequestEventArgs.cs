namespace Rainbow.Testing.Boomerang.Host
{
    using System;

    using Fiddler;

    public class ProxyRequestEventArgs : EventArgs
    {
        public Session Session { get; set; }
    }
}