namespace boomerang.tests.unit
{
    using NSubstitute;

    using NUnit.Framework;

    using Rainbow.Testing.Boomerang.Host;

    public class WhenConfiguringPutRequests
    {
        [Test]
        public void Should_add_put_request()
        {
            var boom = new BoomarangImpl(Substitute.For<IMasqarade>());
            boom.Put("address1", "data");

            boom.ThenShouldHaveRegisteredNumberOfRequests(1);
            boom.ThenShouldContainRequest("PUT", "/address1");
        }

        [Test]
        public void Should_add_response()
        {
            var boom = new BoomarangImpl(Substitute.For<IMasqarade>());
            boom.Put("address1", "data");
            boom.Returns("body", 200);

            boom.ThenShouldHaveRegisteredNumberOfResponses(1);
            boom.ThenShouldContainPutResponse("/address1", "body");
        }
    }
}