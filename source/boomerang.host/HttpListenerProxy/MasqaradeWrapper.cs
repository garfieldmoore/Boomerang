namespace Rainbow.Testing.Boomerang.Host.HttpListenerProxy
{
    using System;
    using System.IO;
    using System.Net;

    internal class MasqaradeWrapper : IMasqarade
    {
        private readonly Server server;

        public event EventHandler<ProxyRequestEventArgs> BeforeRequest;

        public MasqaradeWrapper(Server server)
        {
            this.server = server;
            server.ReceivedRequest += ServerReceivedRequest;

        }

        public void Start(int portNumber)
        {
            //TODO remove static localhost
            server.Start(string.Format("http://localhost:{0}", portNumber));
        }

        public void Stop()
        {
            server.Stop();
        }

        public void SetResponse(Response response)
        {
            server.WriteResponse(response.Body, response.StatusCode, response.Headers, response.ContentType, response.CacheControl);
        }

        public void Start(string address)
        {
            server.Start(address);
        }

        private void ServerReceivedRequest(object sender, HttpListenerRequestArgs e)
        {
            var body = GetRequerstBody(e.Request);
            BeforeRequest(this, new ProxyRequestEventArgs() { Method = e.Request.HttpMethod, RelativePath = e.Request.RawUrl, Body = body });
        }

        private static string GetRequerstBody(HttpListenerRequest request)
        {
            string body = string.Empty;
            using (var reader = new StreamReader(request.InputStream))
            {
                body = reader.ReadToEnd();
            }
            return body;
        }
    }
}