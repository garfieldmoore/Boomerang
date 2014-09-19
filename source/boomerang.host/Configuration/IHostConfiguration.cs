namespace Rainbow.Testing.Boomerang.Host.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHostConfiguration
    {
        void OnPort(int portNumber);

        void UseHostBuilder(IBoomerangConfigurationFactory hostFactory);
    }
}