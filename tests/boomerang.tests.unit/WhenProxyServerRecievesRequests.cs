using NSubstitute;
using NUnit.Framework;
using Rainbow.Testing.Boomerang.Host;
using Shouldly;

namespace boomerang.tests.unit
{
    public class WhenProxyServerRecievesRequests
    {
        private ResponseRepository _responseRepository;

        private BoomarangImpl boomerang;
        private IMasqarade masqarade;

        [Test]
        public void Expected_response_should_be_returned()
        {
            GivenProxyForRequest(new Request {Address = "address", Method = "GET"});
            GivenRegisteredResponse("body", 200);
            GivenProxyIsRunning();

            WhenRequestIsSent(masqarade);

            ThenShouldSetResponse();
        }

        [Test]
        public void Should_fire_recieved_request_event()
        {
            GivenProxyForRequest(new Request {Method = "GET", Address = "address", Body = "mybody"});
            GivenProxyIsRunning();
            bool localArgs = false;
            boomerang.OnReceivedRequest += (sender, args) => localArgs = true;

            masqarade.BeforeRequest +=
                Raise.EventWith(new ProxyRequestEventArgs {Method = "GET", RelativePath = "address", Body = "body"});

            localArgs.ShouldBe(true);
        }

        private static void WhenRequestIsSent(IMasqarade masqarade)
        {
            masqarade.BeforeRequest += Raise.EventWith(
                masqarade, new ProxyRequestEventArgs {Method = "GET", RelativePath = "address"});
        }

        private void GivenRegisteredResponse(string body, int statusCode)
        {
            _responseRepository.AddResponse(body, statusCode);
        }

        private void GivenProxyIsRunning()
        {
            var hostSettings = new HostSettings();
            hostSettings.Prefixes.Add("http:anyaddress");
            boomerang = new BoomarangImpl(masqarade, hostSettings,()=> _responseRepository);
            boomerang.Start();
        }

        private void GivenProxyForRequest(Request request)
        {
            masqarade = Substitute.For<IMasqarade>();
            _responseRepository = new ResponseRepository();
            _responseRepository.AddAddress(request);
        }

        private void ThenShouldSetResponse()
        {
            masqarade.Received().SetResponse(Arg.Any<Response>());
        }
    }
}