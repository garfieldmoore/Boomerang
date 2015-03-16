namespace boomerang.tests.acceptance
{
    using NUnit.Framework;

    using Rainbow.Testing.Boomerang.Host;

    using Shouldly;

    public class StartingHostSpecs
    {
        private IBoomerang _host;

        [Test]
        public void Should_start_host_on_specified_port()
        {
            Spec.GivenAServerOnSpecificPort().Get("address").Returns("started again", 201);

            Spec.WhenGetRequestSent(Spec.HostAddress + "address");
            Spec.StopServer();

            Spec.ResponseText.ShouldBe("started again");
        }

        [Test, Ignore]
        public void Should_automatically_select_port()
        {
            _host.Get("address").Returns("started", 201);

            Spec.WhenGetRequestSent(Spec.HostAddress + "address");
            Spec.StopServer();
            Spec.ResponseText.ShouldBe("started");
        }
    }
}