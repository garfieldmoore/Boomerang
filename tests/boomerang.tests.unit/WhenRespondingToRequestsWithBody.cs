namespace boomerang.tests.unit
{
    using NUnit.Framework;

    using Rainbow.Testing.Boomerang.Host;

    using Shouldly;

    public class WhenRespondingToRequestsWithBody
    {
        [Test]
        public void Should_find_response_for_request_and_body()
        {
            var responder = new RequestResponses();
            responder.AddAddress(new Request() { Address = "address", Method = "HEAD", Body = "my body" });
            responder.AddResponse("my new body", 200);

            var response = responder.GetNextResponseFor("HEAD", "address", "my body");

            response.StatusCode.ShouldBe(200);
            response.Body.ShouldBe("my new body");
        }

        [Test]
        public void Should_not_find_response_for_request_and_different_body()
        {
            var responder = new RequestResponses();
            responder.AddAddress(new Request() { Address = "address", Method = "HEAD", Body = "my body" });
            responder.AddResponse("my new body", 200);

            var response = responder.GetNextResponseFor("HEAD", "address", "different body");

            response.StatusCode.ShouldBe(400);
        }

        [Test]
        public void Should_find_response_for_request_when_body_not_registered_for_address()
        {
            // This is a backward compatibility problem as I originally supported POST and PUTS that did not include a body. 
            // this is wrong but now i am stuck until this behaviour is deprecated.
            var responder = new RequestResponses();
            responder.AddAddress(new Request() { Address = "address", Method = "HEAD" });
            responder.AddResponse("my new body", 200);

            var response = responder.GetNextResponseFor("HEAD", "address", "body");

            response.StatusCode.ShouldBe(200);
        }

        [Test]
        public void Should_not_find_response_for_request_and_registered_body()
        {
            var responder = new RequestResponses();
            responder.AddAddress(new Request() { Address = "address", Method = "HEAD", Body = "body" });
            responder.AddResponse("my new body", 200);

            var response = responder.GetNextResponseFor("HEAD", "address", "");

            response.StatusCode.ShouldBe(400);
        }
    }
}