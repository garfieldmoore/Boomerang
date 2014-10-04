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
            RequestHandlers.Handler = new ResponseRepository();
        }

        /// <summary>
        /// Used for testing
        /// </summary>
        /// <param name="proxy">Proxy server to use</param>
        /// <param name="responses">Responses expected</param>
        public BoomarangImpl(IMasqarade proxy, IResponseRepository responses)
        {
            this.proxy = proxy;
            RequestHandlers.Handler = responses;
        }

        /// <summary>
        ///  Start the proxy server
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
            SetResponse(requesteventArgs.Method, requesteventArgs.RelativePath);
        }

        private void FireReceivedRequest(ProxyRequestEventArgs eventArgs)
        {
            if (OnReceivedRequest != null)
            {
                OnReceivedRequest(this, eventArgs);
            }
        }

        private void SetResponse(string method, string relativePath)
        {
            Response expectedResponse = RequestHandlers.Handler.GetNextResponseFor(method, relativePath);

            proxy.SetResponse(expectedResponse);
        }
    }
}