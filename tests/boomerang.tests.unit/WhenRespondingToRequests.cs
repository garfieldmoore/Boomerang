namespace boomerang.tests.unit
{
    using System.Collections.Generic;

    using FizzWare.NBuilder;

    using NUnit.Framework;

    using Rainbow.Testing.Boomerang.Host;

    using Shouldly;

    public class WhenRespondingToRequests
    {
        [Test]
        public void Should_find_response_for_address()
        {
            var responder = new RequestResponder();

            var responses = Builder<RequestResponse>.CreateListOfSize(1).All().With(x => x.Method = "GET").TheFirst(1).With(x => x.Address = "address1").Build();

            var response = responder.GetResponse("GET", "address1", responses);
            response.ShouldBeSameAs(responses[0]);
        }

        [Test]
        public void Should_find_multiple_repsonses()
        {
            var responder = new RequestResponder();

            var responses = Builder<RequestResponse>.CreateListOfSize(2).All().With(x => x.Method = "GET").TheFirst(1).With(x => x.Address = "address1").TheNext(1).With(x => x.Address = "address2").Build();

            var response = responder.GetResponse("address1", responses);
            response.ShouldBeSameAs(responses[0]);

            response = responder.GetResponse("address2", responses);
            response.ShouldBeSameAs(responses[1]);
        }

        [Test]
        public void Should_not_fail_if_address_not_configured()
        {
            var responder = new RequestResponder();

            var responses = Builder<RequestResponse>.CreateListOfSize(1).All().With(x => x.Method = "GET").Build();

            var response = responder.GetResponse("GET", "address2", responses);
            response.ShouldNotBe(null);
            response.ShouldNotBe(responses[0]);
        }

        [Test]
        public void Should_not_fail_if_no_response_defined_for_address()
        {
            var responder = new RequestResponder();

            var responses = Builder<RequestResponse>.CreateListOfSize(1).All().With(x => x.Method = "GET").Build();

            var response = responder.GetResponse("GET", "address2", responses);
            response.ShouldNotBe(null);
            response.ShouldNotBe(responses[0]);
        }

        [Test]
        public void Should_find_multiple_responses_for_same_address()
        {
            var responder = new RequestResponder();

            var responses = Builder<RequestResponse>.CreateListOfSize(2).All().With(x => x.Method = "GET").With(x => x.Address = "address1").Build();

            var response = responder.GetResponse("GET", "address1", responses);
            response.ShouldBe(responses[0]);

            response = responder.GetResponse("GET", "address1", responses);
            response.ShouldBe(responses[1]);

        }

    }
}