﻿namespace boomerang.tests.acceptance
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

        private static AutoResetEvent waitHandle;

        [Test]
        public void Should_intercept_relative_address()
        {
            Spec.GivenAServerOnSpecificPort().Get("thisaddress").Returns("body", 201);

            Spec.WhenGetRequestSent(Spec.HostAddress +"thisaddress");

            Spec.StatusCode.ShouldBe(HttpStatusCode.Created.ToString());
            Spec.ResponseText.ShouldBe("body");

            Spec.StopServer();

        }

        [Test,Ignore]
        public void Should_record_all_requests()
        {
            int calls = Spec.GivenAServerOnSpecificPort().GetAllReceivedRequests().Count(); // can no longer do this

            Spec.GivenAServerOnSpecificPort().Get("thisaddress").Returns("body", 200);

            Spec.WhenGetRequestSent(Spec.HostAddress + "thisaddress");

            Spec.ReceivedRequests.Count.ShouldBe(calls + 1);
            Spec.ReceivedRequests.Contains(new Request { Method = "GET", Address = "/thisaddress" }).ShouldBe(true);

            Spec.StopServer();

        }

        [Test]
        public void Should_send_bad_request_when_no_responses_configured()
        {
            Spec.GivenAServerOnSpecificPort().Get("thisaddress").Returns("body", 200);

            Spec.WhenGetRequestSent(Spec.HostAddress + "thisaddress");
            Spec.WhenGetRequestSent(Spec.HostAddress + "thisaddress");

            Spec.StatusCode.ShouldBe("BadRequest");
            Spec.ResponseText.ShouldBe("Boomerang error: Resource not found or no response configured for request");

            Spec.StopServer();

        }

        [Test,Ignore]
        public void Should_raise_event_when_request_received()
        {
            receivedRequests = 0;
            Spec.GivenAServerOnSpecificPort().Get("thisaddress").Returns("body", 200);
            Spec.GivenAServerOnSpecificPort().OnReceivedRequest += OnReceivedRequest; // cannot do this to access the events as it restarts

            waitHandle = new AutoResetEvent(false);
            Spec.WhenGetRequestSent(Spec.HostAddress + "thisaddress");

            if (!waitHandle.WaitOne(1000, false))
            {
                Assert.Fail("Test timed out; 'RequestSpecs.Should_raise_event_when_request_received()'");
            }
            Spec.StopServer();

            receivedRequests.ShouldBe(1);
        }

        private static void OnReceivedRequest(object sender, ProxyRequestEventArgs e)
        {
            receivedRequests++;
            waitHandle.Set();
        }

    }
}