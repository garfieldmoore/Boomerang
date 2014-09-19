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
        public void Should_create_host()
        {
            Boomerang.Create(Substitute.For<Action<IHostConfiguration>>()).ShouldBeTypeOf<IBoomerang>();
        }

        [Test]
        public void Should_set_port()
        {
            var proxy = Boomerang.Create(x => x.OnPort(10));
        }

        [Test]
        public void Should_set_fake_factory()
        {
            var proxy = Boomerang.Create(x => x.UseHostBuilder(new DefaultConfigurationFactory()));
        }

        [Test]
        public void Starts_returns_ok()
        {
            var proxy = new BoomarangImpl(Substitute.For<IMasqarade>());
            proxy.Start(0).ShouldBe(BoomerangExitCode.Ok);
        }

    }
}