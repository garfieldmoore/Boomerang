using System.Collections;
using System.Collections.Generic;
using System.Linq;

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

        [Test]
        public void Should_collect_event()
        {
            RequestHandlers.Handler = new ResponseRepository();
            var proxy = Substitute.For<IBoomerang>();
            proxy.Get("address");

            proxy.OnReceivedRequest += Raise.EventWith(proxy, new ProxyRequestEventArgs() { Body = "body", Method = "GET", RelativePath = "address" });

            var requests = proxy.GetAllReceivedRequests().ToArray();
            requests.Length.ShouldBe(1);
        }

        [Test]
        public void Should_collect_event_from_multiple_hosts_independently()
        {
            RequestHandlers.Handler = new ResponseRepository();
            var proxy1 = Substitute.For<IBoomerang>();
            var proxy2 = Substitute.For<IBoomerang>();

            proxy1.Get("address");
            proxy2.Get("address");

            proxy1.OnReceivedRequest += Raise.EventWith(proxy1, new ProxyRequestEventArgs() { Body = "body", Method = "GET", RelativePath = "address" });
            proxy2.OnReceivedRequest += Raise.EventWith(proxy2, new ProxyRequestEventArgs() { Body = "body", Method = "GET", RelativePath = "address" });
            proxy2.OnReceivedRequest += Raise.EventWith(proxy2, new ProxyRequestEventArgs() { Body = "body", Method = "GET", RelativePath = "address" });

            proxy1.GetAllReceivedRequests().ToArray().Length.ShouldBe(1);
            proxy2.GetAllReceivedRequests().ToArray().Length.ShouldBe(2);
        }
    }
}