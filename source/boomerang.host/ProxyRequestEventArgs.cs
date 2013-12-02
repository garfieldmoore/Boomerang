namespace Rainbow.Testing.Boomerang.Host
{
    using System;
    
    public class ProxyRequestEventArgs : EventArgs
    {
        public string Method { get; set; }
        public string RelativePath { get; set; }
    }
}