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
            var boom = Substitute.For<IBoomerang>();
            boom.Get("address1");

            boom.Received(1).AddAddress("address1");
        }

        [Test]
        public void Should_add_response_with_forward_slash()
        {
            var boom = new BoomarangImpl(Substitute.For<IMasqarade>());

            boom.AddAddress("address1");
            boom.RelativeAddresses.ShouldContain("/address1");
        }

        [Test]
        public void Should_not_add_additonal_slash()
        {
            var boom = new BoomarangImpl(Substitute.For<IMasqarade>());

            boom.AddAddress("/address1");
            boom.RelativeAddresses.ShouldContain("/address1");
        }

        [Test]
        public void Should_add_duplicate_address()
        {
            var boom = new BoomarangImpl(Substitute.For<IMasqarade>());

            boom.AddAddress("/address1");
            boom.AddAddress("/address1");

            boom.RelativeAddresses.Count.ShouldBe(2);
        }

        [Test]
        public void Should_add_response()
        {
            var boom = new BoomarangImpl(Substitute.For<IMasqarade>());
            
            boom.Returns("body", 200);
            boom.Responses.ShouldContain(x=>x.StatusCode==200 && x.Body=="body");
        }

        [Test]
        public void Should_be_able_to_add_duplicate_responses()
        {
            var boom = new BoomarangImpl(Substitute.For<IMasqarade>());

            boom.Returns("body", 200);
            boom.Returns("body", 200);
            boom.Responses.Count.ShouldBe(2);
        }
    }
}