namespace boomerang.tests.unit
{
    using NSubstitute;

    using NUnit.Framework;

    using Rainbow.Testing.Boomerang.Host;

    public class WhenConfiguringDeleteRequests
    {
        [Test]
        public void Should_add_delete_request()
        {
            var boom = new BoomarangImpl(Substitute.For<IMasqarade>());
            boom.Delete("address1");

            boom.ThenShouldHaveRegisteredNumberOfRequests(1);
            boom.ThenShouldContainRequest("DELETE", "/address1");
        }


    }
}