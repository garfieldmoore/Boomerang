namespace Rainbow.Testing.Boomerang.Host
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    using Fiddler;

    using System.Linq;

    public class BoomarangImpl : IBoomerang
    {
        private readonly IMasqarade proxy;

        private int listenPort;

        private string listenHost;

        public IList<RequestResponse> RequestResponses;

        public BoomarangImpl(IMasqarade proxy)
        {
            this.proxy = proxy;
            RequestResponses=new List<RequestResponse>();
        }

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
            if (!request.Address.StartsWith("/"))
            {
                request.Address = "/" + request.Address;
            }

            RequestResponses.Add(request);
        }

        public void AddResponse(string body, int statusCode)
        {
            this.RequestResponses[RequestResponses.Count - 1].Response = new Response() { Body = body, StatusCode = statusCode };
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
            string httpResponseStatus = "";
            string responseString="";
            var contentType = "text/html; charset=UTF-8";
            var cacheControl = "private, max-age=0";
            int responseCode=0;

            var responseFinder = new RequestResponder();
            var resonse = responseFinder.GetResponse(session.PathAndQuery, RequestResponses);

            if (resonse.Response != null)
            {
                responseString = resonse.Response.Body;
                httpResponseStatus = resonse.Response.StatusCode.ToString();
                responseCode = resonse.Response.StatusCode;
            }
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
    }
}