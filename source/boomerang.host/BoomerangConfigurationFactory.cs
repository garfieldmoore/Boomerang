namespace Rainbow.Testing.Boomerang.Host
{
    using System;

    using Fiddler;

    public class BoomerangConfigurationFactory : IBoomerangConfigurationFactory
    {
        public IBoomerang Create()
        {
            return new BoomarangImpl(new FiddlerProxy());
        }
    }

    public class ProxyRequestEventArgs : EventArgs
    {
        public Session Session { get; set; }
    }
}