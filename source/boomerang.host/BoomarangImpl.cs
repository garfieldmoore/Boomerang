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

        public RequestResponses Registrations;

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

        /// <summary>
        /// Registers requests as a distict method and address
        /// </summary>
        /// <param name="request">Method and address</param>
        public void AddAddress(Request request)
        {
            this.Registrations.AddAddress(request);
        }

        /// <summary>
        /// Adds a response for the previously added address
        /// </summary>
        /// <param name="body">The response body for the request</param>
        /// <param name="statusCode">The statuc code to respond with</param>
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
            var expectedResponse = Registrations.GetNextResponseFor(session.oRequest.headers.HTTPMethod, session.PathAndQuery);

            proxy.SetResponse(session, expectedResponse);
        }
    }
}