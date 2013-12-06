﻿namespace boomerang.tests.acceptance
{
    using System.Linq;

    using NUnit.Framework;

    using Rainbow.Testing.Boomerang.Host;

    using Shouldly;

    public class RequestSpecs
    {
        private string webHostAddress = "http://localhost:5200/";

        [Test]
        public void Should_record_all_requests()
        {
            int calls = Spec.GivenADefaultServer().GetAllReceivedRequests().Count();

            Spec.GivenADefaultServer().Get("thisaddress").Returns("body", 200);

            Spec.WhenWebGetRequestSent(webHostAddress + "thisaddress");

            Spec.ReceivedRequests.Count.ShouldBe(calls + 1);
            Spec.ReceivedRequests.Contains(new Request() { Method = "GET", Address = "/thisaddress" }).ShouldBe(true);
        }

        [Test]
        public void Should_send_bad_request_when_no_responses_configured()
        {
            Spec.GivenADefaultServer().Get("thisaddress").Returns("body", 200);

            Spec.WhenWebGetRequestSent(webHostAddress + "thisaddress");
            Spec.WhenWebGetRequestSent(webHostAddress + "thisaddress");

            Spec.StatusCode.ShouldBe("BadRequest");
            Spec.ResponseText.ShouldBe("Boomerang error: Resource not found or no response configured for request");
        }
    }
}