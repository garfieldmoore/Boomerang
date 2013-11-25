namespace Rainbow.Testing.Boomerang.Host
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    using Fiddler;

    internal class Boomarang : IBoomerang, ISendBy
    {
        private int listenPort;

        private string listenHost;

        private readonly List<string> relativeAddresses;

        private List<Response> responses;

        public Boomarang()
        {
            this.relativeAddresses = new List<string>();
            this.responses = new List<Response>();
        }

        public void Start(string host, int port)
        {
            this.listenHost = host;
            this.listenPort = port;
            AppDomain.CurrentDomain.DomainUnload += CurrentDomain_DomainUnload;
            var flags = FiddlerCoreStartupFlags.Default;
            FiddlerApplication.Startup(port, flags);
            FiddlerApplication.BeforeRequest += this.FiddlerApplication_BeforeRequest;
        }

        private void CurrentDomain_DomainUnload(object sender, EventArgs e)
        {
            Stop();
        }

        private void FiddlerApplication_BeforeRequest(Session session)
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
            
            if (relativeAddresses.Contains(session.PathAndQuery))
            {
                var loc = relativeAddresses.IndexOf(session.PathAndQuery);
                if (loc <= responses.Count)
                {
                    responseString = responses[loc].Body;
                    httpResponseStatus = responses[loc].StatusCode.ToString();
                    responseCode = responses[loc].StatusCode;
                }
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

        public void AddAddress(string prefix)
        {
            if (prefix.StartsWith(@"/"))
            {
                relativeAddresses.Add(prefix);
            }
            else
            {
                relativeAddresses.Add(@"/" + prefix);

            }
        }

        public IBoomerang Returns(string body, int i)
        {
            responses.Add(new Response() { Body = body, StatusCode = i });

            return this;
        }
    }

    internal class Response
    {
        public int StatusCode { get; set; }

        public string Body { get; set; }
    }
}