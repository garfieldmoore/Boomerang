namespace Rainbow.Testing.Boomerang.Host
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    /// <summary>
    /// Manages connection to proxy server
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class BoomarangImpl : IBoomerang
    {
        private readonly IMasqarade proxy;
        private int listenPort;
        private string listenHost;

        public IRequestResponses Registrations;

        protected IList<Request> ReceivedRequests;

        public BoomarangImpl(IMasqarade proxy)
        {
            ReceivedRequests = new List<Request>();
            this.proxy = proxy;
            this.Registrations = new RequestResponses();
        }

        public BoomarangImpl(IMasqarade proxy, IRequestResponses responses)
        {
            ReceivedRequests = new List<Request>();
            this.proxy = proxy;
            this.Registrations = responses;
        }

        /// <summary>
        /// Start the proxy server
        /// </summary>
        /// <param name="port">The port number to listen on</param>
        public void Start(int port)
        {
            this.listenPort = port;
            AppDomain.CurrentDomain.DomainUnload += this.OnCurrentDomainUnload;
            proxy.Start(port);
            proxy.BeforeRequest += this.OnProxyBeforeRequest;
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

        public void AddResponse(string body, int statusCode, IDictionary<string, string> headers)
        {
            this.Registrations.AddResponse(body, statusCode, headers);

        }

        private void OnProxyBeforeRequest(object sender, EventArgs e)
        {
            var requesteventArgs = e as ProxyRequestEventArgs;
            if (requesteventArgs == null)
            {
                return;
            }

            this.OnBeforeRequest(requesteventArgs);
        }

        private void OnCurrentDomainUnload(object sender, EventArgs e)
        {
            if (proxy != null)
            {
                proxy.Stop();
            }
        }

        private void OnBeforeRequest(ProxyRequestEventArgs eventArgs)
        {
            // Should probably raise an event for this..
            var request = new Request() { Method = eventArgs.Method, Address = eventArgs.RelativePath };

            ReceivedRequests.Add(request);

            SetResponse(eventArgs.Method, eventArgs.RelativePath);
        }

        private void SetResponse(string method, string relativePath)
        {
            var expectedResponse = Registrations.GetNextResponseFor(method, relativePath);

            proxy.SetResponse(expectedResponse);
        }

        public IEnumerable<Request> GetAllReceivedRequests()
        {
            return ReceivedRequests;
        }
    }
}