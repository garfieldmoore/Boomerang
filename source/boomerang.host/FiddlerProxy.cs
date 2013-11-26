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