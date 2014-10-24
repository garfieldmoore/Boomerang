namespace boomerang.tests.unit
{
    using NSubstitute;

    using NUnit.Framework;

    using Rainbow.Testing.Boomerang.Host;

    using Shouldly;

    public class CollectingEventExtensionTests
    {
        [Test]
        public void Should_return_empty_list_when_no_requests_made()
        {
            var proxy = Substitute.For<IBoomerang>();
            proxy.GetAllReceivedRequests().ShouldNotBe(null);
        }
 
    }
}