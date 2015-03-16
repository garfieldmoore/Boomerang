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
            var hostSettings = new HostSettings();
            hostSettings.Prefixes.Add("http://localhost");
            var proxy = new BoomarangImpl(Substitute.For<IMasqarade>(), hostSettings);

            proxy.Start().ShouldBe(BoomerangExitCode.Ok);
        }
    }
}