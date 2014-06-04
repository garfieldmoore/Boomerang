namespace Rainbow.Testing.Boomerang.Host
{
    /// <summary>
    /// Interface for Boomerang implementations
    /// </summary>
    public interface IBoomerangConfigurationFactory
    {
        /// <summary>
        /// Creates the instance
        /// </summary>
        /// <returns>An instance of IBoomerang proxy</returns>
        IMasqarade Create();
    }
}