namespace boomerang.tests.acceptance
{
    using System.Linq;
    using System.Net;
    using System.Threading;

    using NUnit.Framework;

    using Rainbow.Testing.Boomerang.Host;

    using Shouldly;

    public class RequestSpecs
    {
        private static int receivedRequests;

        [Test, Ignore]
        public void Should_intercept_relative_address_on_any_base_address()
        {
            Spec.GivenAServerOnSpecificPort().Get("thisaddress").Returns("body", 201);

            Spec.WhenGetRequestSent("http://www.UnknownBaseAddress/thisaddress");

            Spec.StatusCode.ShouldBe(HttpStatusCode.Created.ToString());
            Spec.ResponseText.ShouldBe("body");
        }

        [Test]
        public void Should_record_all_requests()
        {
            int calls = Spec.GivenAServerOnSpecificPort().GetAllReceivedRequests().Count();

            Spec.GivenAServerOnSpecificPort().Get("thisaddress").Returns("body", 200);

            Spec.WhenGetRequestSent(Spec.HostAddress + "thisaddress");

            Spec.ReceivedRequests.Count.ShouldBe(calls + 1);
            Spec.ReceivedRequests.Contains(new Request { Method = "GET", Address = "/thisaddress" }).ShouldBe(true);
        }

        [Test]
        public void Should_send_bad_request_when_no_responses_configured()
        {
            Spec.GivenAServerOnSpecificPort().Get("thisaddress").Returns("body", 200);

            Spec.WhenGetRequestSent(Spec.HostAddress + "thisaddress");
            Spec.WhenGetRequestSent(Spec.HostAddress + "thisaddress");

            Spec.StatusCode.ShouldBe("BadRequest");
            Spec.ResponseText.ShouldBe("Boomerang error: Resource not found or no response configured for request");
        }

        [Test]
        public void Should_raise_event_when_request_received()
        {
            receivedRequests = 0;
            Spec.GivenAServerOnSpecificPort().Get("thisaddress").Returns("body", 200);
            Spec.GivenAServerOnSpecificPort().OnReceivedRequest += OnReceivedRequest;

            Spec.WhenGetRequestSent(Spec.HostAddress + "thisaddress");
            // TODO how to make this test better??...
            Thread.Sleep(100);
            receivedRequests.ShouldBe(1);
        }

        private static void OnReceivedRequest(object sender, ProxyRequestEventArgs e)
        {
            receivedRequests++;
        }

    }
}