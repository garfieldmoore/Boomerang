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
            var responder = new RequestResponder();
            responder.AddAddress(new RequestResponse() { Address = "address", Method = "GET" });
            responder.AddResponse("body1", 200);

            var response = responder.GetResponse("GET", "address");
            
            response.Response.Body.ShouldBe("body1");
            response.Response.StatusCode.ShouldBe(200);
        }

        [Test]
        public void Should_reponse_with_resource_not_found_if_address_not_configured()
        {
            var responder = new RequestResponder();
            responder.AddAddress(new RequestResponse() { Address = "address", Method = "GET" });
            responder.AddResponse("body1", 200);

            var response = responder.GetResponse("GET", "address2");
            
            response.ShouldNotBe(null);
            response.Response.Body.ShouldBe(RequestResponder.ResourceNotFoundMessage);
            response.Response.StatusCode.ShouldBe(400);
        }

        [Test]
        public void Should_not_fail_if_no_response_defined_for_address()
        {
            var responder = new RequestResponder();
            responder.AddAddress(new RequestResponse() { Address = "address", Method = "GET" });
            responder.AddResponse("body1", 200);

            var response = responder.GetResponse("GET", "address");
            response.ShouldNotBe(null);
            response.Response.Body.ShouldBe("body1");
            response.Response.StatusCode.ShouldBe(200);
        }

        [Test]
        public void Should_find_multiple_responses_for_same_address()
        {
            var responder = new RequestResponder();
            responder.AddAddress(new RequestResponse() { Address = "address", Method = "GET" });
            responder.AddResponse("body1", 200);
            responder.AddResponse("body2", 201);

            var response = responder.GetResponse("address");
            
            response.Response.StatusCode.ShouldBe(200);
            response.Response.Body.ShouldBe("body1");

            response = responder.GetResponse("GET", "address");
            response.Response.StatusCode.ShouldBe(201);
            response.Response.Body.ShouldBe("body2");
        }
    }
}