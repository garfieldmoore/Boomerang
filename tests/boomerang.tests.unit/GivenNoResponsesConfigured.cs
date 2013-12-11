namespace boomerang.tests.unit
{
    using System.Linq;
    using NSubstitute;
    using NUnit.Framework;
    using Rainbow.Testing.Boomerang.Host;
    using Shouldly;

    public class GivenMoreRequestsThanResponses
    {
        private RequestResponses requestResponses;
        private IMasqarade masqarade;
        private BoomarangImpl boomerang;

        [Test]
        public void When_no_more_responses_the_should_record_request_and_send_bad_request()
        {
            this.GivenProxyServer();
            requestResponses.AddAddress(new Request() { Method = "GET", Address = "ADDRESS" });
            GivenProxyIsRunning();

            WhenRequestIsSent(masqarade);

            ThenShouldStoreRequest();
        }

        private void GivenRegisteredResponse(string body, int statusCode)
        {
            this.requestResponses.AddResponse(body, statusCode);
        }

        private void GivenProxyIsRunning()
        {
            this.boomerang = this.boomerang = new BoomarangImpl(this.masqarade, this.requestResponses);
            this.boomerang.Start(5100);
        }

        private void GivenProxyServer()
        {
            this.masqarade = Substitute.For<IMasqarade>();
            this.requestResponses = new RequestResponses();
        }

        private void ThenShouldStoreRequest()
        {
            boomerang.GetAllReceivedRequests().Count().ShouldBe(1);
        }

        private static void WhenRequestIsSent(IMasqarade masqarade)
        {
            masqarade.BeforeRequest += Raise.EventWith(
                masqarade, new ProxyRequestEventArgs() { Method = "GET", RelativePath = "address" });
        }


    }
}