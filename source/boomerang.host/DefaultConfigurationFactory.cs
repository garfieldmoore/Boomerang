namespace Rainbow.Testing.Boomerang.Host
{
    using System.ComponentModel;

    /// <summary>
    /// Default proxy server factory
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class DefaultConfigurationFactory : IBoomerangConfigurationFactory
    {
        /// <summary>
        /// Create boomerang instance
        /// </summary>
        /// <returns>Returns an instance of IBoomerang</returns>        
        public IBoomerang Create()
        {
            return new BoomarangImpl(new FiddlerProxy());
        }
    }
}