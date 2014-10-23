namespace boomerang.tests.acceptance.HostConfiguration
{
    using System.Collections.Generic;
    using System.Linq;

    using NUnit.Framework;

    using Rainbow.Testing.Boomerang.Host;

    using Shouldly;

    public class CollectingEventsWithEventMethod
    {
        private List<ProxyRequestEventArgs> requests;

        [Test]
        public void Should_collect_events()
        {
            requests = new List<ProxyRequestEventArgs>();
            var p = Boomerang.Create(x => x.AtAddress("http://localhost:5604/"));

            p.Get("test").Returns("ran a test", 201);
            p.OnReceivedRequest += OnReceivedRequest;
            p.Start();
            Spec.WhenGetRequestSent("http://localhost:5604/test");

            requests.Count().ShouldBe(1);
            p.Stop();
        }

        void OnReceivedRequest(object sender, ProxyRequestEventArgs e)
        {
            requests.Add(e);
        }
    }
}