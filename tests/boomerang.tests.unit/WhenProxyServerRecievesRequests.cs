namespace boomerang.tests.unit
{
    using NSubstitute;
    using NUnit.Framework;
    using Rainbow.Testing.Boomerang.Host;

    public class WhenProxyServerRecievesRequests
    {
        [Test]
        public void Boomerang_should_set_proxy_response()
        {
            var masqarade = Substitute.For<IMasqarade>();
            var requestResponses = Substitute.For<IRequestResponses>();
            requestResponses.GetNextResponseFor("GET", "address").Returns(new Response());

            var boom = new BoomarangImpl(masqarade, requestResponses);
            boom.Start("http://localhost", 5100);

            masqarade.BeforeRequest += Raise.EventWith(masqarade, new ProxyRequestEventArgs() { Method="GET", RelativePath = "address" });

            masqarade.Received().SetResponse(Arg.Any<Response>());
        }
    }
}