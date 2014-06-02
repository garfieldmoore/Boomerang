using System.Linq;

namespace boomerang.tests.unit
{
    using System.Collections.Generic;

    using NUnit.Framework;

    using Rainbow.Testing.Boomerang.Host;

    using Shouldly;

    public class WhenRespondingToRequests
    {
        [Test]
        public void Should_find_response_for_address()
        {
            var responder = new RequestResponses();
            responder.AddAddress(new Request() { Address = "address", Method = "GET" });
            responder.AddResponse("body1", 200);

            var response = responder.GetNextResponseFor("GET", "address");

            response.Body.ShouldBe("body1");
            response.StatusCode.ShouldBe(200);
        }

        [Test]
        public void Should_reponse_with_resource_not_found_if_address_not_configured()
        {
            var responder = new RequestResponses();
            responder.AddAddress(new Request() { Address = "address", Method = "GET" });
            responder.AddResponse("body1", 200);

            var response = responder.GetNextResponseFor("GET", "address2");

            response.ShouldNotBe(null);
            response.Body.ShouldBe(RequestResponses.ResourceNotFoundMessage);
            response.StatusCode.ShouldBe(400);
        }

        [Test]
        public void Should_not_fail_if_no_response_defined_for_address()
        {
            var responder = new RequestResponses();
            responder.AddAddress(new Request() { Address = "address", Method = "GET" });
            responder.AddResponse("body1", 200);

            var response = responder.GetNextResponseFor("GET", "address");
            response.ShouldNotBe(null);
            response.Body.ShouldBe("body1");
            response.StatusCode.ShouldBe(200);
        }

        [Test]
        public void Should_find_multiple_responses_for_same_address()
        {
            var responder = new RequestResponses();
            responder.AddAddress(new Request() { Address = "address", Method = "GET" });
            responder.AddResponse("body1", 200);
            responder.AddResponse("body2", 201);

            var response = responder.GetNextResponseFor("GET", "address");

            response.StatusCode.ShouldBe(200);
            response.Body.ShouldBe("body1");

            response = responder.GetNextResponseFor("GET", "address");
            response.StatusCode.ShouldBe(201);
            response.Body.ShouldBe("body2");
        }

        [Test]
        public void Changes_header_reference_should_not_affect_response_headers()
        {
            var responder = new RequestResponses();
            responder.AddAddress(new Request() { Address = "address", Method = "GET" });
            var headers = new Dictionary<string, string>() { { "key", "value" } };
            responder.AddResponse("body", 201, headers);

            headers.Remove("key");

            var response = responder.GetNextResponseFor("GET", "address");

            response.Headers.Count.ShouldBe(1);
        }

        [Test]
        public void Should_set_response_headers()
        {
            var responder = new RequestResponses();
            responder.AddAddress(new Request() { Address = "address", Method = "GET" });
            var headers = new Dictionary<string, string>() { { "content-type", "application/json" } };
            responder.AddResponse("body", 201, headers);

            var response = responder.GetNextResponseFor("GET", "address");
            response.Headers.Values.ToList()[0].ShouldBe("application/json");
        }
    }
}