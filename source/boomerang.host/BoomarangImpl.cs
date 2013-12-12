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
        public IRequestResponses Registrations;

        protected IList<Request> ReceivedRequests;

        private readonly IMasqarade proxy;

        /// <summary>
        /// Creates a a manager for the supplied proxy server implementation
        /// </summary>
        /// <param name="proxy">proxy server implementation</param>
        public BoomarangImpl(IMasqarade proxy)
        {
            ReceivedRequests = new List<Request>();
            this.proxy = proxy;
            Registrations = new RequestResponses();
        }

        /// <summary>
        /// Used for testing
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="responses"></param>
        public BoomarangImpl(IMasqarade proxy, IRequestResponses responses)
        {
            ReceivedRequests = new List<Request>();
            this.proxy = proxy;
            Registrations = responses;
        }

        /// <summary>
        /// Registers a HTTP method for interception at a relative address
        /// </summary>
        /// <param name="request">Method and address</param>
        public void AddAddress(Request request)
        {
            Registrations.AddAddress(request);
        }

        /// <summary>
        ///     Adds a response for the previously added address
        /// </summary>
        /// <param name="body">The response body for the request</param>
        /// <param name="statusCode">The status code to respond with</param>
        public void AddResponse(string body, int statusCode)
        {
            Registrations.AddResponse(body, statusCode);
        }

        public void AddResponse(string body, int statusCode, IDictionary<string, string> headers)
        {
            Registrations.AddResponse(body, statusCode, headers);
        }

        public IEnumerable<Request> GetAllReceivedRequests()
        {
            return ReceivedRequests;
        }

        /// <summary>
        ///     Start the proxy server
        /// </summary>
        /// <param name="port">The port number to listen on</param>
        public void Start(int port)
        {
            AppDomain.CurrentDomain.DomainUnload += OnCurrentDomainUnload;
            proxy.Start(port);
            proxy.BeforeRequest += OnProxyBeforeRequest;
        }

        private void OnBeforeRequest(ProxyRequestEventArgs eventArgs)
        {
            // Should probably raise an event for this..
            var request = new Request { Method = eventArgs.Method, Address = eventArgs.RelativePath };

            ReceivedRequests.Add(request);

            SetResponse(eventArgs.Method, eventArgs.RelativePath);
        }

        private void OnCurrentDomainUnload(object sender, EventArgs e)
        {
            if (proxy != null)
            {
                proxy.Stop();
            }
        }

        private void OnProxyBeforeRequest(object sender, EventArgs e)
        {
            var requesteventArgs = e as ProxyRequestEventArgs;
            if (requesteventArgs == null)
            {
                return;
            }

            OnBeforeRequest(requesteventArgs);
        }

        private void SetResponse(string method, string relativePath)
        {
            Response expectedResponse = Registrations.GetNextResponseFor(method, relativePath);

            proxy.SetResponse(expectedResponse);
        }
    }
}