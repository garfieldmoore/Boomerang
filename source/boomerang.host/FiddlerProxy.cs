namespace Rainbow.Testing.Boomerang.Host
{
    using System;
    using System.Diagnostics;
    using System.Threading;

    using Fiddler;

    /// <summary>
    /// Proxy server using FiddlerCore API
    /// </summary>
    internal class FiddlerProxy : IMasqarade
    {
        private int listenPort;
        private Session currentSession;

        /// <summary>
        /// Fires when a session receives a request
        /// </summary>
        public event EventHandler BeforeRequest;

        /// <summary>
        /// Stop the proxy server
        /// </summary>
        public void Stop()
        {
            if (FiddlerApplication.oProxy != null)
            {
                if (FiddlerApplication.oProxy.IsAttached)
                {
                    FiddlerApplication.oProxy.Detach();
                }

                Thread.Sleep(500);
                FiddlerApplication.Shutdown();
            }
        }

        /// <summary>
        /// Sets the response for the current sessions request
        /// </summary>
        /// <param name="response">The desired response</param>
        public void SetResponse(Response response)
        {
            currentSession.utilCreateResponseAndBypassServer();
            currentSession.oResponse.headers.HTTPResponseStatus = response.StatusCode.ToString();
            currentSession.oResponse.headers.HTTPResponseCode = response.StatusCode;

            SetHeaders(response);

            currentSession.utilSetResponseBody(response.Body);
        }

        /// <summary>
        /// Starts the proxy
        /// </summary>
        /// <param name="portNumber">The port number the proxy will listen on. Zero to select an unused port</param>
        public void Start(int portNumber)
        {
            var flags = FiddlerCoreStartupFlags.Default ^ FiddlerCoreStartupFlags.RegisterAsSystemProxy;
            FiddlerApplication.Startup(portNumber, flags);
            listenPort = FiddlerApplication.oProxy.ListenPort;
            FiddlerApplication.BeforeRequest += this.OnBeforeRequest;
        }

        /// <summary>
        /// Fired before a request is processed
        /// </summary>
        /// <param name="session">The session that owns the request</param>
        protected virtual void OnBeforeRequest(Session session)
        {
            currentSession = session;
            if (HasSubscribers(BeforeRequest) && IsRequestOwner(session))
            {
                BeforeRequest(this, CreateRequestEventArgs(session));
            }
        }

        private static bool HasSubscribers(EventHandler handler)
        {
            return handler != null;
        }

        private static ProxyRequestEventArgs CreateRequestEventArgs(Session session)
        {
            return new ProxyRequestEventArgs() { Method = session.RequestMethod, RelativePath = session.PathAndQuery };
        }

        private void SetHeaders(Response response)
        {
            if (response.Headers.Count == 0)
            {
                this.currentSession.oResponse["Content-Type"] = response.ContentType;
                this.currentSession.oResponse["Cache-Control"] = response.CacheControl;
            }
            else
            {
                foreach (var header in response.Headers)
                {
                    this.currentSession.oResponse.headers.Add(header.Key, header.Value);
                }
            }
        }

        private bool IsRequestOwner(Session session)
        {
            return session.oRequest.pipeClient.LocalPort == this.listenPort;
        }
    }
}