namespace Rainbow.Testing.Boomerang.Host
{
    /// <summary>
    /// Interface for Boomerang implementations
    /// </summary>
    internal interface IBoomerangConfigurationFactory
    {
        IBoomerang Create();
    }
}