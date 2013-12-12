namespace Rainbow.Testing.Boomerang.Host
{
    /// <summary>
    /// Default proxy server factory
    /// </summary>
    internal class BoomerangConfigurationFactory : IBoomerangConfigurationFactory
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