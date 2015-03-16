using NSubstitute.Routing.Handlers;

namespace boomerang.tests.unit
{
    using System;

    using NSubstitute;

    using NUnit.Framework;

    using Rainbow.Testing.Boomerang.Host;

    public class WhenStartingServer
    {
        private IBoomerangConfigurationFactory boomerangConfigurationFactory;

        private IMasqarade proxy;

        [Test]
        public void Should_call_proxy_start()
        {
            var proxy = Substitute.For<IMasqarade>();
            var boomer = new BoomarangImpl(proxy);

            boomer.Start();

            proxy.Received(1).Start(@"http://localhost:5100");
        }

        [Test]
        public void Should_start_on_default_port()
        {
            GivenConfigurationFactoryCreatesProxyListener();

            Boomerang.Create(x => x.UseHostBuilder(() => proxy));

            proxy.Received(1).Start("http://localhost:5100");
        }

        [Test]
        public void Should_stop_proxy_when_reinitialising()
        {
            GivenConfigurationFactoryCreatesProxyListener();

            Boomerang.Create(x => x.UseHostBuilder(() => proxy));

            proxy.Received(1).Start("http://localhost:5100");

            proxy.Received(1).Stop();
        }

        private void GivenConfigurationFactoryCreatesProxyListener()
        {
            boomerangConfigurationFactory = Substitute.For<IBoomerangConfigurationFactory>();
            proxy = Substitute.For<IMasqarade>();
            boomerangConfigurationFactory.Create().Returns(proxy);
        }
    }
}