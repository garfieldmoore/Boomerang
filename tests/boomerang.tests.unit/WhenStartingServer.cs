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
            var hostSettings = new HostSettings();
            hostSettings.Prefixes.Add("http://localhost:5100");
            var boomer = new BoomarangImpl(proxy, hostSettings);

            boomer.Start();

            proxy.Received(1).Start(@"http://localhost:5100");
        }
    }
}