namespace boomerang.tests.unit
{
    using NSubstitute;

    using NUnit.Framework;

    using Rainbow.Testing.Boomerang.Host;


    public class WhenConfiguringGetRequests
    {
        [Test]
        public void Should_add_address()
        {
            var boom = new BoomarangImpl(Substitute.For<IMasqarade>());
            boom.Get("address1");

            boom.ThenShouldHaveRegisteredNumberOfRequests(1);
            boom.ThenShouldContainRequestWithAddress("/address1");
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
        public void Should_add_multiple_repsonses_for_single_address()
        {
            var boom = new BoomarangImpl(Substitute.For<IMasqarade>());

            boom.Get("/address1");
            boom.AddResponse("body1", 200);
            boom.AddResponse("body1", 200);

            boom.ThenShouldHaveRegisteredNumberOfResponses(2);
        }
    }
}