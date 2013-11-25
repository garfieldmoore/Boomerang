namespace Rainbow.Testing.Boomerang.Host
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    using Fiddler;

    public class BoomarangImpl : IBoomerang, ISendBy
    {
        private readonly IMasqarade proxy;

        private int listenPort;

        private string listenHost;

        public readonly List<string> RelativeAddresses;

        public List<Response> Responses;

        public BoomarangImpl(IMasqarade proxy)
        {
            this.proxy = proxy;
            this.RelativeAddresses = new List<string>();
            this.Responses = new List<Response>();
        }

        public void Start(string host, int port)
        {
            this.listenHost = host;
            this.listenPort = port;
            AppDomain.CurrentDomain.DomainUnload += CurrentDomain_DomainUnload;
            proxy.Start(host, port);
            proxy.BeforeRequest += proxy_BeforeRequest;
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
            Stop();
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
            string httpResponseStatus;
            string responseString;
            var contentType = "text/html; charset=UTF-8";
            httpResponseStatus = "Ok";
            responseString = "Boomerang interception";
            var cacheControl = "private, max-age=0";
            int responseCode = 200;

            var responseFinder = new RequestResponder();
            var resonse = responseFinder.GetResponse(session.PathAndQuery, Responses, RelativeAddresses);

            responseString = resonse.Body;
            httpResponseStatus = resonse.StatusCode.ToString();
            responseCode = resonse.StatusCode;

            session.utilCreateResponseAndBypassServer();
            session.oResponse.headers.HTTPResponseStatus = httpResponseStatus;
            session.oResponse.headers.HTTPResponseCode = responseCode;
            session.oResponse["Content-Type"] = contentType;
            session.oResponse["Cache-Control"] = cacheControl;
            session.utilSetResponseBody(responseString);
        }

        public void Stop()
        {
            FiddlerApplication.oProxy.Detach();
            Thread.Sleep(500);
            FiddlerApplication.Shutdown();
        }

        public void AddAddress(string prefix)
        {
            if (prefix.StartsWith(@"/"))
            {
                this.RelativeAddresses.Add(prefix);
            }
            else
            {
                this.RelativeAddresses.Add(@"/" + prefix);

            }
        }

        public IBoomerang Returns(string body, int i)
        {
            this.Responses.Add(new Response() { Body = body, StatusCode = i });

            return this;
        }
    }

    public class Response
    {
        public int StatusCode { get; set; }

        public string Body { get; set; }
    }
}