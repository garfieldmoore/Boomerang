namespace Rainbow.Testing.Boomerang.Host.Configuration
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public interface IHostConfiguration
    {
        void UseHostBuilder(Func<IMasqarade> hostFactory);

        void AtAddress(string url);

        void UseStaticResponseRequestHandler();
    }
}