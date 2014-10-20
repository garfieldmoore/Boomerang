namespace boomerang.tests.acceptance.HostConfiguration
{
    using System;
    using System.Net;

    using NUnit.Framework;

    using Rainbow.Testing.Boomerang.Host;

    using Shouldly;

    [TestFixture]
    public class ConfigurationBuilderSpecs
    {
        [Test]
        public void Should_configure_address()
        {
            var proxy = Boomerang.Create(
                x =>
                    { x.AtAddress("http://localhost:5602/"); });

            proxy.Get("newtest").Returns("test 1", 201);
            proxy.Start();

            Spec.WhenGetRequestSent("http://localhost:5602/newtest");
            Spec.StatusCode.ShouldBe(HttpStatusCode.Created.ToString());
            Spec.ResponseText.ShouldBe("test 1");
            proxy.Stop();
        }

        [Test]
        public void Should_configure_default_responses()
        {
            var proxy = Boomerang.Create(x =>
                {
                    x.AtAddress("http://localhost:5602/");
                    x.UseSingleResponsePerRequestHandler();
                });
            proxy.Get("singleresponse").Returns("singleresponse", 200);

            proxy.Start();

            Spec.WhenGetRequestSent("http://localhost:5602/singleresponse");
            Spec.StatusCode.ShouldBe(HttpStatusCode.OK.ToString());
            Spec.ResponseText.ShouldBe("singleresponse");

            Spec.WhenGetRequestSent("http://localhost:5602/singleresponse");
            Spec.StatusCode.ShouldBe(HttpStatusCode.OK.ToString());
            Spec.ResponseText.ShouldBe("singleresponse");
            proxy.Stop();
        }

        [Test]
        public void Should_close_connection_when_stopped()
        {
            var proxy = Boomerang.Create(x =>
            {
                x.AtAddress("http://localhost:5601/");
                
            });

            proxy.Start();
            proxy.Stop();

            var proxy2 = Boomerang.Create(x =>
            {
                x.AtAddress("http://localhost:5601/");
            });

            proxy2.Start();
            proxy2.Stop();
        }

    }
}