namespace boomerang.tests.acceptance
{
    using NUnit.Framework;

    using Rainbow.Testing.Boomerang.Host;

    using Shouldly;

    public class StartingHostSpecs
    {
        [Test]
        public void Should_start_host_on_specified_port()
        {
            Boomerang.Initialize(new DefaultConfigurationFactory());
            Boomerang.Server(5000).Get("address").Returns("started again", 201);

            Spec.WhenGetRequestSent("http://localhost:5000/" + "address");

            Spec.ResponseText.ShouldBe("started again");
            Spec.GivenAServerOnSpecificPort();
        }

        [Test, Ignore]
        public void Should_automatically_select_port()
        {
            Boomerang.Initialize(new DefaultConfigurationFactory());
            Boomerang.Server().Get("address").Returns("started", 201);

            Spec.WhenGetRequestSent(Spec.HostAddress + "address");

            Spec.ResponseText.ShouldBe("started");
        }

        [Test]
        public void Should_shutdown_proxy_before_restarting()
        {
            Boomerang.Initialize(new DefaultConfigurationFactory());
            Boomerang.Server().Get("address").Returns("started again", 201);

            Spec.WhenGetRequestSent(Spec.HostAddress + "address");

            Boomerang.Initialize(new DefaultConfigurationFactory());
            Boomerang.Server().Get("address").Returns("started", 201);
        }
    }
}