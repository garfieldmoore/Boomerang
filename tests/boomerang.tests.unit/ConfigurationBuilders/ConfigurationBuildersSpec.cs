namespace boomerang.tests.unit.ConfigurationBuilders
{
    using System;

    using NSubstitute;

    using NUnit.Framework;

    using Rainbow.Testing.Boomerang.Host;
    using Rainbow.Testing.Boomerang.Host.Configuration;
    using Rainbow.Testing.Boomerang.Host.HttpListenerProxy;

    using Shouldly;

    public class ConfigurationBuildersSpec
    {
        private IMasqarade webProxy;

        private IBoomerang proxy;

        private string serverHostPrefix;

        [Test]
        public void Should_create_host()
        {
            Boomerang.Create(Substitute.For<Action<IHostConfiguration>>()).ShouldBeTypeOf<IBoomerang>();
        }

        [Test]
        public void Start_invokes_proxy_with_endpoint()
        {
            GivenServerConfiguredWithEndpoint();

            WhenServerIsStarted();

            webProxy.Received(1).Start(serverHostPrefix);
        }

        private void WhenServerIsStarted()
        {
            proxy.Start();
        }

        private void GivenServerConfiguredWithEndpoint()
        {
            webProxy = Substitute.For<IMasqarade>();
            proxy = Boomerang.Create(
                x =>
                {
                    x.UseHostBuilder(() => webProxy);
                    serverHostPrefix = "http://localhost/index/";
                    x.AtAddress(serverHostPrefix);
                });
        }
    }
}