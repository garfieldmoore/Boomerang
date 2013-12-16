namespace boomerang.tests.unit
{
    using System;

    using NSubstitute;

    using NUnit.Framework;

    using Rainbow.Testing.Boomerang.Host;

    public class WhenStartingServer
    {
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
            var boomerangConfigurationFactory = Substitute.For<IBoomerangConfigurationFactory>();
            var proxy = Substitute.For<BoomarangImpl>();
            boomerangConfigurationFactory.Create().Returns(proxy);

            Boomerang.Initialize(boomerangConfigurationFactory);
            Boomerang.Server();

            proxy.Received(1).Start(0);
        }
    }
}