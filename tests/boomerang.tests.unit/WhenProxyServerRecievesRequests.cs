namespace boomerang.tests.unit
{
    using NSubstitute;
    using NUnit.Framework;
    using Rainbow.Testing.Boomerang.Host;

    public class WhenProxyServerRecievesRequests
    {
        private RequestResponses requestResponses;
        private IMasqarade masqarade;
        private BoomarangImpl boomerang;

        [Test]
        public void Expected_response_should_be_returned()
        {
            GivenProxyForRequest(new Request() { Address = "address", Method = "GET" });
            GivenRegisteredResponse("body", 200);
            GivenProxyIsRunning();

            WhenBeforeRequestIsSent(masqarade);

            ThenShouldSetResponse();
        }

        private void GivenRegisteredResponse(string body, int statusCode)
        {
            this.requestResponses.AddResponse(body, statusCode);
        }

        private void GivenProxyIsRunning()
        {
            this.boomerang = this.boomerang = new BoomarangImpl(this.masqarade, this.requestResponses);
            this.boomerang.Start("http://localhost", 5100);
        }

        private void GivenProxyForRequest(Request request)
        {
            this.masqarade = Substitute.For<IMasqarade>();
            this.requestResponses = new RequestResponses();
            this.requestResponses.AddAddress(request);
        }

        private void ThenShouldSetResponse()
        {
            masqarade.Received().SetResponse(Arg.Any<Response>());
        }

        private static void WhenBeforeRequestIsSent(IMasqarade masqarade)
        {
            masqarade.BeforeRequest += Raise.EventWith(
                masqarade, new ProxyRequestEventArgs() { Method = "GET", RelativePath = "address" });
        }
    }
}