namespace Rainbow.Testing.Boomerang.Host
{
    /// <summary>
    /// Interface for Boomerang implementations
    /// </summary>
    internal interface IBoomerangConfigurationFactory
    {
        /// <summary>
        /// Creates the instance
        /// </summary>
        /// <returns>An instance of IBoomerang</returns>
        IBoomerang Create();
    }
}