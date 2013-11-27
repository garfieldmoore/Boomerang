namespace Rainbow.Testing.Boomerang.Host
{
    using System;
    using System.Threading;

    using Fiddler;

    public class FiddlerProxy : IMasqarade
    {
        public event EventHandler BeforeRequest;

        public void Stop()
        {
            FiddlerApplication.oProxy.Detach();
            Thread.Sleep(500);
            FiddlerApplication.Shutdown();

        }

        public void SetResponse(Session session, Response response)
        {
            string httpResponseStatus = "";
            string responseString = "";
            var contentType = "text/html; charset=UTF-8";
            var cacheControl = "private, max-age=0";
            int responseCode = 0;

            if (response != null)
            {
                responseString = response.Body;
                httpResponseStatus = response.StatusCode.ToString();
                responseCode = response.StatusCode;
            }

            session.utilCreateResponseAndBypassServer();
            session.oResponse.headers.HTTPResponseStatus = httpResponseStatus;
            session.oResponse.headers.HTTPResponseCode = responseCode;
            session.oResponse["Content-Type"] = contentType;
            session.oResponse["Cache-Control"] = cacheControl;
            session.utilSetResponseBody(responseString);
        }

        protected virtual void OnBeforeRequest(Session oSession)
        {
            var handler = this.BeforeRequest;
            if (handler != null)
            {
                handler(this, new ProxyRequestEventArgs() { Session = oSession });
            }
        }

        public void Start(string hostBaseAddress, int portNumber)
        {
            var flags = FiddlerCoreStartupFlags.Default | FiddlerCoreStartupFlags.RegisterAsSystemProxy;

            FiddlerApplication.Startup(portNumber, flags);
            FiddlerApplication.BeforeRequest += this.OnBeforeRequest;
        }
    }
}