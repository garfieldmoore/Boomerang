namespace Rainbow.Testing.Boomerang.Host.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHostConfiguration
    {
        void UseHostBuilder(IBoomerangConfigurationFactory hostFactory);

        void AtAddress(string url);

        void AlwaysRespondWithLastConfiguredResponse();
    }
}