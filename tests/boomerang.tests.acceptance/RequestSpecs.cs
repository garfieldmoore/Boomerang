namespace boomerang.tests.acceptance
{
    using System.Linq;
    using System.Net;
    using System.Threading;

    using NUnit.Framework;

    using Newtonsoft.Json;

    using Rainbow.Testing.Boomerang.Host;

    using Shouldly;

    public class RequestSpecs
    {
        private static int receivedRequests;

        private static AutoResetEvent waitHandle;

        [Test]
        public void Should_intercept_relative_address()
        {
            Spec.GivenAServerOnSpecificPort().Get("thisaddress").Returns("body", 201);

            Spec.WhenGetRequestSent(Spec.HostAddress + "thisaddress");

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

            waitHandle = new AutoResetEvent(false);
            Spec.WhenGetRequestSent(Spec.HostAddress + "thisaddress");

            if (!waitHandle.WaitOne(1000, false))
            {
                Assert.Fail("Test timed out; 'RequestSpecs.Should_raise_event_when_request_received()'");
            }

            receivedRequests.ShouldBe(1);
        }

        [Test]
        public void Should_intercept_relative_address_with_body()
        {
            Spec.GivenAServerOnSpecificPort().Request("address", "POST", JsonConvert.SerializeObject("body")).Returns("new body", 201);
            Spec.WhenPostsSentTo(Spec.HostAddress + "address", "body");
            Spec.StatusCode.ShouldBe("Created");
            Spec.ResponseText.ShouldBe("new body");
        }

        [Test]
        public void Should_return_not_configured_error_when_no_body_matched()
        {
            Spec.GivenAServerOnSpecificPort().Request("address", "POST", JsonConvert.SerializeObject("body")).Returns("new body", 201);
            Spec.WhenPostsSentTo(Spec.HostAddress + "address", "body1");

            Spec.StatusCode.ShouldBe("BadRequest");
            Spec.ResponseText.ShouldContain("Boomerang error");
        }

        private static void OnReceivedRequest(object sender, ProxyRequestEventArgs e)
        {
            receivedRequests++;
            waitHandle.Set();
        }

    }
}