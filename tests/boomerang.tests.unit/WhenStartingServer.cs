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

            boomer.Start("localhost", 5100);

            proxy.Received(1).Start("localhost", 5100);
        }
    }
}