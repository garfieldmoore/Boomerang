namespace Rainbow.Testing.Boomerang.Host
{
    using System;

    using Fiddler;

    public class BoomarangImpl : IBoomerang
    {
        private readonly IMasqarade proxy;

        private int listenPort;

        private string listenHost;

        public RequestResponder Registrations;

        public BoomarangImpl(IMasqarade proxy)
        {
            this.proxy = proxy;
            this.Registrations = new RequestResponder();
        }

        public RequestResponder Registerer { get; protected set; }

        public void Start(string host, int port)
        {
            this.listenHost = host;
            this.listenPort = port;
            AppDomain.CurrentDomain.DomainUnload += CurrentDomain_DomainUnload;
            proxy.Start(host, port);
            proxy.BeforeRequest += proxy_BeforeRequest;
        }

        public void AddAddress(RequestResponse request)
        {
            this.Registrations.AddAddress(request);
        }

        public void AddResponse(string body, int statusCode)
        {
            this.Registrations.AddResponse(body, statusCode);
        }

        private void proxy_BeforeRequest(object sender, EventArgs e)
        {
            var requesteventArgs = e as ProxyRequestEventArgs;
            if (requesteventArgs == null)
            {
                return;
            }

            this.OnBeforeRequest(requesteventArgs.Session);
        }

        private void CurrentDomain_DomainUnload(object sender, EventArgs e)
        {
            if (proxy != null)
            {
                proxy.Stop();
            }

        }

        private void OnBeforeRequest(Session session)
        {
            if ((session.oRequest.pipeClient.LocalPort == this.listenPort) && (session.hostname == this.listenHost))
            {
                SetResponse(session);
            }
        }

        private void SetResponse(Session session)
        {
            var resonse = this.Registrations.GetResponse(session.oRequest.headers.HTTPMethod, session.PathAndQuery);

            proxy.SetResponse(session, resonse.Response);
        }
    }
}