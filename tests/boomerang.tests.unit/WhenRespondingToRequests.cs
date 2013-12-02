namespace boomerang.tests.unit
{
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
    }
}