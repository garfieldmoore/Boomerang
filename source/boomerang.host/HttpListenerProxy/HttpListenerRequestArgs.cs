namespace Rainbow.Testing.Boomerang.Host.HttpListenerProxy
{
    using System;
    using System.Net;

    internal class HttpListenerRequestArgs : EventArgs
    {
        public HttpListenerRequestArgs(HttpListenerRequest request)
        {
            Request = request;
        }

        public HttpListenerRequest Request { get; set; }
    }
}