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

            var address = new List<string>();
            address.Add("address1");

            var responses = Builder<Response>.CreateListOfSize(1).Build();

            var response = responder.GetResponse("address1", responses, address);
            response.ShouldBeSameAs(responses[0]);
        }

        [Test]
        public void Should_find_multiple_repsonses()
        {
            var responder = new RequestResponder();

            var address = new List<string>();
            address.Add("address1");
            address.Add("address2");

            var responses = Builder<Response>.CreateListOfSize(2).Build();

            var response = responder.GetResponse("address1", responses, address);
            response.ShouldBeSameAs(responses[0]);

            response = responder.GetResponse("address2", responses, address);
            response.ShouldBeSameAs(responses[1]);
        }

        [Test]
        public void Should_not_fail_if_address_not_configured()
        {
            var responder = new RequestResponder();

            var address = new List<string>();
            address.Add("address1");

            var responses = Builder<Response>.CreateListOfSize(1).Build();

            var response = responder.GetResponse("address2", responses, address);
            response.ShouldNotBe(null);
            response.ShouldNotBe(responses[0]);
        }

        [Test]
        public void Should_not_fail_if_no_response_defined_for_address()
        {
            var responder = new RequestResponder();

            var address = new List<string>();
            address.Add("address1");
            address.Add("address2");

            var responses = Builder<Response>.CreateListOfSize(1).Build();

            var response = responder.GetResponse("address2", responses, address);
            response.ShouldNotBe(null);
            response.ShouldNotBe(responses[0]);
        }
    }
}