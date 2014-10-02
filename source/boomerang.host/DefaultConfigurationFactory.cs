namespace Rainbow.Testing.Boomerang.Host
{
    using System.ComponentModel;

    using Rainbow.Testing.Boomerang.Host.HttpListenerProxy;

    /// <summary>
    /// Default proxy server factory
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class DefaultConfigurationFactory : IBoomerangConfigurationFactory
    {
        /// <summary>
        /// Create default proxy instance
        /// </summary>
        /// <returns>Returns an instance of proxy</returns>        
        public IMasqarade Create()
        {
            //return new FiddlerProxy();
            return new HttpListenerFactory().Create();
        }
    }
}