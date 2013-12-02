namespace Rainbow.Testing.Boomerang.Host
{
    public class BoomerangConfigurationFactory : IBoomerangConfigurationFactory
    {
        public IBoomerang Create()
        {
            return new BoomarangImpl(new FiddlerProxy());
        }
    }
}