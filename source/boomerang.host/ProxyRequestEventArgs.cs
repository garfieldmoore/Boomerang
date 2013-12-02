namespace Rainbow.Testing.Boomerang.Host
{
    using System;

    using Fiddler;

    //  not needed if rr is in fiddler
    public class ProxyRequestEventArgs : EventArgs
    {
        public Session Session { get; set; }
    }
}