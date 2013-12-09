namespace Rainbow.Testing.Boomerang.Host
{
    using System;
    using System.Threading;
    using Fiddler;

    internal class FiddlerProxy : IMasqarade
    {
        public event EventHandler BeforeRequest;

        private int listenPort;
        private string listenHost;
        private Session currentSession;

        public void Stop()
        {
            FiddlerApplication.oProxy.Detach();
            Thread.Sleep(500);
            FiddlerApplication.Shutdown();
        }

        public void SetResponse(Response response)
        {
            currentSession.utilCreateResponseAndBypassServer();
            currentSession.oResponse.headers.HTTPResponseStatus = response.StatusCode.ToString();
            currentSession.oResponse.headers.HTTPResponseCode = response.StatusCode;

            SetHeaders(response);

            currentSession.utilSetResponseBody(response.Body);
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

        protected virtual void OnBeforeRequest(Session session)
        {
            currentSession = session;
            if (HasSubscribers(BeforeRequest) && (IsRequestOwner(session)))
            {
                BeforeRequest(this, CreateRequestEventArgs(session));
            }
        }

        private static bool HasSubscribers(EventHandler handler)
        {
            return handler != null;
        }

        private bool IsRequestOwner(Session session)
        {
            return (session.oRequest.pipeClient.LocalPort == this.listenPort) && (session.hostname == this.listenHost);
        }

        private static ProxyRequestEventArgs CreateRequestEventArgs(Session session)
        {
            return new ProxyRequestEventArgs()
                       {
                           Method = session.RequestMethod,
                           RelativePath = session.PathAndQuery
                       };
        }

        public void Start(string hostBaseAddress, int portNumber)
        {
            var flags = FiddlerCoreStartupFlags.Default | FiddlerCoreStartupFlags.RegisterAsSystemProxy;
            listenPort = portNumber;
            this.listenHost = hostBaseAddress;
            FiddlerApplication.Startup(portNumber, flags);
            FiddlerApplication.BeforeRequest += this.OnBeforeRequest;
        }
    }
}