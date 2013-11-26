namespace boomerang.tests.unit
{
    using NSubstitute;

    using NUnit.Framework;

    using Rainbow.Testing.Boomerang.Host;

    using Shouldly;

    public class WhenConfiguringGetRequests
    {
        [Test]
        public void Should_add_address()
        {
            var boom = new BoomarangImpl(Substitute.For<IMasqarade>());
            boom.Get("address1");

            boom.ThenShouldHaveRegisteredNumberOfRequests(1);
            boom.ThenShouldContainRequestWithAddress("/address1");
            boom.ThenShouldContainRequest(x=>x.Method == "GET" && x.Address=="/address1");
        }

        [Test]
        public void Should_add_response_with_forward_slash()
        {
            var boom = new BoomarangImpl(Substitute.For<IMasqarade>());

            boom.Get("address1");
            boom.ThenShouldContainRequestWithAddress("/address1");
        }

        [Test]
        public void Should_not_add_additonal_slash()
        {
            var boom = new BoomarangImpl(Substitute.For<IMasqarade>());

            boom.Get("/address1");
            boom.ThenShouldContainRequestWithAddress("/address1");
        }

        [Test]
        public void Should_add_duplicate_address()
        {
            var boom = new BoomarangImpl(Substitute.For<IMasqarade>());

            boom.Get("/address1");
            boom.Get("/address1");

            boom.ThenShouldHaveRegisteredNumberOfResponses(2);
        }

        [Test]
        public void Should_add_response()
        {
            var boom = new BoomarangImpl(Substitute.For<IMasqarade>());

            boom.Get("address");
            boom.Returns("body", 200);
            boom.ThenShouldContainResponse(x => x.Response.StatusCode == 200 && x.Response.Body == "body");
        }
    }
}