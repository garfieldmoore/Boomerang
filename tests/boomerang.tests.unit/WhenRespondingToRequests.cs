namespace boomerang.tests.unit
{

    using FizzWare.NBuilder;

    using NUnit.Framework;

    using Rainbow.Testing.Boomerang.Host;

    using Shouldly;

    public class WhenRespondingToRequests
    {
        [Test]
        public void Should_find_response_for_address()
        {
            var responses = Builder<RequestResponse>.CreateListOfSize(1).All().With(x => x.Method = "GET").TheFirst(1).With(x => x.Address = "address1").Build();
            var responder = new RequestResponder(responses);

            var response = responder.GetResponse("GET", "address1");
            response.ShouldBeSameAs(responses[0]);
        }

        [Test]
        public void Should_find_multiple_repsonses()
        {
            var responses = Builder<RequestResponse>.CreateListOfSize(2).All().With(x => x.Method = "GET").TheFirst(1).With(x => x.Address = "address1").TheNext(1).With(x => x.Address = "address2").Build();
            var responder = new RequestResponder(responses);

            var response = responder.GetResponse("address1");
            response.ShouldBeSameAs(responses[0]);

            response = responder.GetResponse("address2");
            response.ShouldBeSameAs(responses[1]);
        }

        [Test]
        public void Should_not_fail_if_address_not_configured()
        {
            var responses = Builder<RequestResponse>.CreateListOfSize(1).All().With(x => x.Method = "GET").Build();
            var responder = new RequestResponder(responses);

            var response = responder.GetResponse("GET", "address2");
            response.ShouldNotBe(null);
            response.ShouldNotBe(responses[0]);
        }

        [Test]
        public void Should_not_fail_if_no_response_defined_for_address()
        {
            var responses = Builder<RequestResponse>.CreateListOfSize(1).All().With(x => x.Method = "GET").Build();
            var responder = new RequestResponder(responses);

            var response = responder.GetResponse("GET", "address2");
            response.ShouldNotBe(null);
            response.ShouldNotBe(responses[0]);
        }

        [Test]
        public void Should_find_multiple_responses_for_same_address()
        {
            var responses = Builder<RequestResponse>.CreateListOfSize(2).All().With(x => x.Method = "GET").With(x => x.Address = "address1").Build();
            var responder = new RequestResponder(responses);

            var response = responder.GetResponse("GET", "address1");
            response.ShouldBe(responses[0]);

            response = responder.GetResponse("GET", "address1");
            response.ShouldBe(responses[1]);
        }

    }
}