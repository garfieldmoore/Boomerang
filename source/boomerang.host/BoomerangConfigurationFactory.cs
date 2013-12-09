namespace Rainbow.Testing.Boomerang.Host
{
    /// <summary>
    /// Default proxy server factory
    /// </summary>
    internal class BoomerangConfigurationFactory : IBoomerangConfigurationFactory
    {
        public IBoomerang Create()
        {
            return new BoomarangImpl(new FiddlerProxy());
        }
    }
}