namespace boomerang.tests.unit.Configurations
{
    using System;

    using NSubstitute;

    using NUnit.Framework;

    using Rainbow.Testing.Boomerang.Host;
    using Rainbow.Testing.Boomerang.Host.Configuration;

    using Shouldly;

    public class HostConfigurationTests
    {
        [Test]
        public void Starts_returns_ok()
        {
            var proxy = new BoomarangImpl(Substitute.For<IMasqarade>());
            proxy.Start(0).ShouldBe(BoomerangExitCode.Ok);
        }
    }
}