namespace Rainbow.Testing.Boomerang.Host.HttpListenerProxy
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;

    internal class HttpListenerFactory : IBoomerangConfigurationFactory
    {
        public IMasqarade Create()
        {
            return new MasqaradeWrapper(Server.Create());
        }
    }

    internal class Server : IDisposable
    {
        private HttpListener listener;

        private HttpListenerContext context;

        private bool running;

        private bool isDisposed;

        public event EventHandler<HttpListenerRequestArgs> ReceivedRequest;

        public bool IsListening()
        {
            if (listener == null) return false;

            return listener.IsListening;
        }

        protected Server()
        {
            running = true;
        }

        protected void OnReceivedRequest(Server server, HttpListenerRequestArgs httpListenerRequestArgs)
        {
            if (ReceivedRequest != null)
            {
                ReceivedRequest(this, httpListenerRequestArgs);
            }
        }

        public void Start(string url)
        {
            if (!HttpListener.IsSupported)
            {
                throw new OsVersionException("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
            }

            // URI prefixes are required, 
            // for example "http://+/contoso.com".
            var prefixes = new[] { url };

            listener = new HttpListener();

            AddPrefixes(prefixes, listener);
            listener.Start();
            Task.Factory.StartNew(() =>
            {
                while (running)
                {
                    context = listener.GetContext();
                    OnReceivedRequest(this, new HttpListenerRequestArgs(context.Request));
                }
            });
        }

        private static void AddPrefixes(string[] prefixes, HttpListener listener)
        {
            foreach (string s in prefixes)
            {
                if (!s.EndsWith("/"))
                {
                    listener.Prefixes.Add(s + "/");
                }
                else
                {
                    listener.Prefixes.Add(s);
                }
            }
        }

        public static Server Create()
        {
            return new Server();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (isDisposed) return;

            if (disposing)
            {
                Stop();
            }

            isDisposed = true;
        }

        public void Stop()
        {
            running = false;
            if (null != listener && listener.IsListening)
            {
                try
                {
                    listener.Stop();
                }
                catch (Exception e)
                {
                    listener.Close();
                }
            }
        }

        private void WriteResponse(string body, int statusCode, HttpListenerContext context, IDictionary<string, string> headers, string contentType, string cachecontrol)
        {
            var response = context.Response;
            SetHeaders(response, headers, contentType, cachecontrol);
            string responseString = body;
            response.StatusCode = statusCode;
            var buffer = System.Text.Encoding.UTF8.GetBytes(responseString);

            response.ContentLength64 = buffer.Length;

            var output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
        }

        public void WriteResponse(string body, int statusCode, IDictionary<string, string> headers, string contentType, string cacheControl)
        {
            WriteResponse(body, statusCode, context, headers, contentType, cacheControl);
        }

        private void SetHeaders(HttpListenerResponse response, IDictionary<string, string> headers, string contentType, string cachecontrol)
        {
            if (headers.Count == 0)
            {
                response.AddHeader("Content-Type", contentType);
                response.AddHeader("Cache-Control", cachecontrol);
            }
            else
            {
                foreach (var header in headers)
                {
                    response.AddHeader(header.Key, header.Value);
                }
            }
        }
    }
}