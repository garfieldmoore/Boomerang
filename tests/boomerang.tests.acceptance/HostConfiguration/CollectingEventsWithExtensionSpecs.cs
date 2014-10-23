namespace boomerang.tests.acceptance.HostConfiguration
{
    using System.Linq;

    using NUnit.Framework;

    using Rainbow.Testing.Boomerang.Host;

    using Shouldly;

    public class CollectingEventsWithExtensionSpecs
    {
        [Test]
        public void Should_collect_events()
        {
            var p = Boomerang.Create(x => x.AtAddress("http://localhost:5603/"));
            p.ClearReceivedRequests();

            p.Get("test").Returns("ran a test", 201);
            p.Start();
            Spec.WhenGetRequestSent("http://localhost:5603/test");

            p.GetAllReceivedRequests().Count().ShouldBe(1);
            p.Stop();
        }

        [Test]
        public void Should_collect_events_on_all_instances()
        {
            var p = Boomerang.Create(x => x.AtAddress("http://localhost:5603/"));
            p.ClearReceivedRequests();

            p.Get("test").Returns("ran a test", 201);
            p.Start();
            Spec.WhenGetRequestSent("http://localhost:5603/test");
            p.Stop();

            p = Boomerang.Create(x => x.AtAddress("http://localhost:5603/"));
            p.ClearReceivedRequests();

            p.Get("test").Returns("ran a test", 201);
            p.Start();
            Spec.WhenGetRequestSent("http://localhost:5603/test");

            p.GetAllReceivedRequests().Count().ShouldBe(1);
        }
    }
}