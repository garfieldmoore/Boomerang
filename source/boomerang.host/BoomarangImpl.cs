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
        /// <summary>
        /// Address and responses
        /// </summary>
        public IRequestResponses Registrations;

        /// <summary>
        /// Fired when requests are received and before the request is processed
        /// </summary>
        public event EventHandler<ProxyRequestEventArgs> OnReceivedRequest;

        private readonly IMasqarade proxy;

        /// <summary>
        /// Default constructor
        /// </summary>
        public BoomarangImpl()
        {
        }

        /// <summary>
        /// Creates a a manager for the supplied proxy server implementation
        /// </summary>
        /// <param name="proxy">proxy server implementation</param>
        public BoomarangImpl(IMasqarade proxy)
        {
            this.proxy = proxy;
            Registrations = new RequestResponses();
        }

        /// <summary>
        /// Used for testing
        /// </summary>
        /// <param name="proxy">Proxy server to use</param>
        /// <param name="responses">Responses expected</param>
        public BoomarangImpl(IMasqarade proxy, IRequestResponses responses)
        {
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

        /// <summary>
        ///     Adds a response for the previously added address
        /// </summary>
        /// <param name="body">The response body for the request</param>
        /// <param name="statusCode">The status code to respond with</param>
        /// <param name="headers">Headers to add. These will replace the defaults</param>
        public void AddResponse(string body, int statusCode, IDictionary<string, string> headers)
        {
            Registrations.AddResponse(body, statusCode, headers);
        }

        /// <summary>
        ///     Start the proxy server
        /// </summary>
        /// <param name="port">The port number to listen on</param>
        public virtual void Start(int port)
        {
            AppDomain.CurrentDomain.DomainUnload += OnCurrentDomainUnload;
            proxy.Start(port);
            proxy.BeforeRequest += OnProxyBeforeRequest;
        }

        /// <summary>
        /// Stops the proxy server
        /// </summary>
        public virtual void Stop()
        {
            if (proxy != null)
            {
                proxy.Stop();
            }
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

            FireReceivedRequest(requesteventArgs);
            SetResponse(requesteventArgs.Method, requesteventArgs.RelativePath, requesteventArgs.Body);
        }

        private void FireReceivedRequest(ProxyRequestEventArgs eventArgs)
        {
            if (OnReceivedRequest != null)
            {
                OnReceivedRequest(this, eventArgs);
            }
        }

        private void SetResponse(string method, string relativePath, object body)
        {
            Response expectedResponse = Registrations.GetNextResponseFor(method, relativePath, body);

            proxy.SetResponse(expectedResponse);
        }
    }
}