namespace boomerang.tests.acceptance.HostConfiguration
{
    using System.Net;

    using NUnit.Framework;

    using Rainbow.Testing.Boomerang.Host;

    using Shouldly;

    [TestFixture]
    public class ConfigurationBuilders
    {
        [Test]
        public void Should_configure_address()
        {
            var proxy = Boomerang.Create(
                x =>
                    { x.AtAddress("http://localhost:5600/"); });

            proxy.Get("newtest").Returns("test 1", 201);
            proxy.Start();

            Spec.WhenGetRequestSent("http://localhost:5600/newtest");
            Spec.StatusCode.ShouldBe(HttpStatusCode.Created.ToString());
            Spec.ResponseText.ShouldBe("test 1");
        }

        [Test]
        public void Should_configure_default_responses()
        {
            var proxy = Boomerang.Create(x =>
                {
                    x.AtAddress("http://localhost:9900/");
                    x.UseSingleResponsePerRequestHandler();
                });
            proxy.Get("singleresponse").Returns("singleresponse", 200);

            proxy.Start();

            Spec.WhenGetRequestSent("http://localhost:9900/singleresponse");
            Spec.StatusCode.ShouldBe(HttpStatusCode.OK.ToString());
            Spec.ResponseText.ShouldBe("singleresponse");

            Spec.WhenGetRequestSent("http://localhost:9900/singleresponse");
            Spec.StatusCode.ShouldBe(HttpStatusCode.OK.ToString());
            Spec.ResponseText.ShouldBe("singleresponse");

        }

    }
}