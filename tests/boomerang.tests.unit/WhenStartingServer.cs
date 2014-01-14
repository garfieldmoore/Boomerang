namespace boomerang.tests.unit
{
    using System;

    using NSubstitute;

    using NUnit.Framework;

    using Rainbow.Testing.Boomerang.Host;

    public class WhenStartingServer
    {
        private IBoomerangConfigurationFactory boomerangConfigurationFactory;

        private BoomarangImpl boomerang;

        [Test]
        public void Should_call_proxy_start()
        {
            var proxy = Substitute.For<IMasqarade>();
            var boomer = new BoomarangImpl(proxy);

            boomer.Start(5100);

            proxy.Received(1).Start(5100);
        }

        [Test]
        public void Should_auto_select_unused_port()
        {
            GivenConfigurationFactoryCreatesProxyListener();

            Boomerang.Initialize(boomerangConfigurationFactory);
            Boomerang.Server();

            boomerang.Received(1).Start(0);
        }

        [Test]
        public void Should_create_new_proxy_after_reinitialise()
        {
            GivenConfigurationFactoryCreatesProxyListener();

            Boomerang.Initialize(boomerangConfigurationFactory);
            Boomerang.Server();

            Boomerang.Initialize(boomerangConfigurationFactory);
            Boomerang.Server();

            boomerangConfigurationFactory.Received(2).Create();
        }

        [Test]
        public void Should_stop_proxy_when_reinitialising()
        {
            GivenConfigurationFactoryCreatesProxyListener();

            Boomerang.Initialize(boomerangConfigurationFactory);
            Boomerang.Server();

            Boomerang.Initialize(boomerangConfigurationFactory);
            Boomerang.Server();

            boomerang.Received(1).Stop();
        }

        private void GivenConfigurationFactoryCreatesProxyListener()
        {
            boomerangConfigurationFactory = Substitute.For<IBoomerangConfigurationFactory>();
            boomerang = Substitute.For<BoomarangImpl>();
            boomerangConfigurationFactory.Create().Returns(boomerang);
        }
    }
}