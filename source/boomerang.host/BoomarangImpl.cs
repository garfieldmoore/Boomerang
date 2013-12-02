namespace Rainbow.Testing.Boomerang.Host
{
    using System;

    using Fiddler;

    /// <summary>
    /// Manages connection to proxy server
    /// </summary>
    public class BoomarangImpl : IBoomerang
    {
        private readonly IMasqarade proxy;
        private int listenPort;
        private string listenHost;

        // This probably needs to be moved up into the fiddler class 
        public RequestResponses Registrations;

        // use factory with initialiase to create this in tests.
        public BoomarangImpl(IMasqarade proxy)
        {
            this.proxy = proxy;
            this.Registrations = new RequestResponses();
        }

        /// <summary>
        /// Start the proxy server
        /// </summary>
        /// <param name="host">host address of the server (defaults to http://localhost:[port]</param>
        /// <param name="port">The port number to listen on</param>
        public void Start(string host, int port)
        {
            this.listenHost = host;
            this.listenPort = port;
            AppDomain.CurrentDomain.DomainUnload += CurrentDomain_DomainUnload;
            proxy.Start(host, port);
            proxy.BeforeRequest += proxy_BeforeRequest;
        }

        public void AddAddress(Request request)
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

        // MOve this to the fiddler class as this is implementation
        private void OnBeforeRequest(Session session)
        {
            if ((session.oRequest.pipeClient.LocalPort == this.listenPort) && (session.hostname == this.listenHost))
            {
                SetResponse(session);
            }
        }

        // feature envy
        private void SetResponse(Session session)
        {
            var expectedResponse = Registrations.GetNextResponseFor(session.oRequest.headers.HTTPMethod, session.PathAndQuery);

            proxy.SetResponse(session, expectedResponse);
        }
    }
}