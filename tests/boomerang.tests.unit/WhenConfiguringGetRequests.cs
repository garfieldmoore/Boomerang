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

            boom.RequestResponses.Count.ShouldBe(1);
            boom.RequestResponses[0].Address.ShouldBe("/address1");
            boom.RequestResponses[0].Method.ShouldBe("GET");
        }

        [Test]
        public void Should_add_response_with_forward_slash()
        {
            var boom = new BoomarangImpl(Substitute.For<IMasqarade>());

            boom.Get("address1");
            boom.RequestResponses[0].Address.ShouldBe("/address1");
        }

        [Test]
        public void Should_not_add_additonal_slash()
        {
            var boom = new BoomarangImpl(Substitute.For<IMasqarade>());

            boom.Get("/address1");
            boom.RequestResponses[0].Address.ShouldContain("/address1");
        }

        [Test]
        public void Should_add_duplicate_address()
        {
            var boom = new BoomarangImpl(Substitute.For<IMasqarade>());

            boom.Get("/address1");
            boom.Get("/address1");

            boom.RequestResponses.Count.ShouldBe(2);
        }

        [Test]
        public void Should_add_response()
        {
            var boom = new BoomarangImpl(Substitute.For<IMasqarade>());

            boom.Get("address");
            boom.Returns("body", 200);
            boom.RequestResponses.ShouldContain(x=>x.Response.StatusCode==200 && x.Response.Body=="body");
        }
    }
 }