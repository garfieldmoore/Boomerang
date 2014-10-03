namespace boomerang.tests.unit.ConfigurationBuilders
{
    using System;

    using NSubstitute;

    using NUnit.Framework;

    using Rainbow.Testing.Boomerang.Host;
    using Rainbow.Testing.Boomerang.Host.Configuration;

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
            proxy = Boomerang.Create(
                x =>
                {
                    var boomerangConfigurationFactory = Substitute.For<IBoomerangConfigurationFactory>();
                    webProxy = Substitute.For<IMasqarade>();
                    boomerangConfigurationFactory.Create().Returns(webProxy);
                    x.UseHostBuilder(boomerangConfigurationFactory);
                    serverHostPrefix = "http://localhost/index/";
                    x.AtAddress(serverHostPrefix);
                });
        }
    }
}