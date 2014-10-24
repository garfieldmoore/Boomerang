namespace boomerang.tests.acceptance.HostConfiguration
{
    using System.Linq;

    using NUnit.Framework;

    using Rainbow.Testing.Boomerang.Host;

    using Shouldly;

    public class CollectingEventsWithExtensionSpecs
    {
        [Test]
        public void Should_collect_requests()
        {
            var p = Boomerang.Create(x => x.AtAddress("http://localhost:5603/"));

            p.Get("test").Returns("ran a test", 201);
            p.Start();
            Spec.WhenGetRequestSent("http://localhost:5603/test");

            p.GetAllReceivedRequests().Count().ShouldBe(1);
            p.Stop();
        }               

        [Test]
        public void Should_associate_events_with_proxy_instance()
        {
            var p = Boomerang.Create(x => x.AtAddress("http://localhost:5603/"));            

            p.Get("test").Returns("ran a test", 201);
            p.Start();
            Spec.WhenGetRequestSent("http://localhost:5603/test");
            p.GetAllReceivedRequests().Count().ShouldBe(1);
            p.Stop();

            p = Boomerang.Create(x => x.AtAddress("http://localhost:5603/"));

            p.Get("test").Returns("ran a test", 201);
            p.Start();
            Spec.WhenGetRequestSent("http://localhost:5603/test");

            p.GetAllReceivedRequests().Count().ShouldBe(1);
            p.Stop();
        }
    }
}