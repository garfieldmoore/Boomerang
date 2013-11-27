namespace boomerang.tests.unit
{
    using NSubstitute;

    using NUnit.Framework;

    using Rainbow.Testing.Boomerang.Host;

    public class WhenConfiguringPosts
    {
        [Test]
        public void Should_add_post_request()
        {
            var boom = new BoomarangImpl(Substitute.For<IMasqarade>());
            boom.Post("address1", "data");

            boom.ThenShouldHaveRegisteredNumberOfRequests(1);
            boom.ThenShouldContainRequest("POST", "address1");
        }

        [Test]
        public void Should_add_response()
        {
            var boom = new BoomarangImpl(Substitute.For<IMasqarade>());
            boom.Post("address1", "data");
            boom.Returns(200, "body");

            boom.ThenShouldHaveRegisteredNumberOfResponses(1);
            boom.ThenShouldContainPostResponse("/address1", "body");
        }

    }
}