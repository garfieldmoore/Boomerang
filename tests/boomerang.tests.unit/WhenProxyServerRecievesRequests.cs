namespace boomerang.tests.unit
{
    using System.Linq;
    using System.Threading;

    using NSubstitute;

    using NUnit.Framework;

    using Rainbow.Testing.Boomerang.Host;

    using Shouldly;

    public class WhenProxyServerRecievesRequests
    {
        private ResponseRepository _responseRepository;

        private IMasqarade masqarade;

        private BoomarangImpl boomerang;

        [Test]
        public void Expected_response_should_be_returned()
        {
            GivenProxyForRequest(new Request() { Address = "address", Method = "GET" });
            GivenRegisteredResponse("body", 200);
            GivenProxyIsRunning();

            WhenRequestIsSent(masqarade);

            ThenShouldSetResponse();
        }

        [Test]
        public void Should_fire_recieved_request_event()
        {
            GivenProxyForRequest(new Request() { Method = "GET", Address = "address", Body = "mybody" });
            GivenProxyIsRunning();
            bool localArgs=false;
            boomerang.OnReceivedRequest += (sender, args) => localArgs = true;

            masqarade.BeforeRequest +=
                Raise.EventWith(new ProxyRequestEventArgs() { Method = "GET", RelativePath = "address",Body="body" });
            
            localArgs.ShouldBe(true);
        }

        private static void WhenRequestIsSent(IMasqarade masqarade)
        {
            masqarade.BeforeRequest += Raise.EventWith(
                masqarade, new ProxyRequestEventArgs() { Method = "GET", RelativePath = "address" });
        }

        private void GivenRegisteredResponse(string body, int statusCode)
        {
            this._responseRepository.AddResponse(body, statusCode);
        }

        private void GivenProxyIsRunning()
        {
            this.boomerang = new BoomarangImpl(this.masqarade, this._responseRepository);
            this.boomerang.Start(5100);
        }

        private void GivenProxyForRequest(Request request)
        {
            this.masqarade = Substitute.For<IMasqarade>();
            this._responseRepository = new ResponseRepository();
            this._responseRepository.AddAddress(request);
        }

        private void ThenShouldSetResponse()
        {
            masqarade.Received().SetResponse(Arg.Any<Response>());
        }
    }
}