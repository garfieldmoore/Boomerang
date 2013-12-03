namespace boomerang.tests.acceptance
{
    using System.Linq;

    using NUnit.Framework;

    using Rainbow.Testing.Boomerang.Host;

    using Shouldly;

    public class RequestSpecs
    {
        private string webHostAddress = "http://localhost:5200/";

        [Test]
        public void Should_record_all_requests()
        {
            int calls = Spec.GivenADefaultServer().GetAllReceivedRequests().Count();

            Spec.GivenADefaultServer().Get("thisaddress").Returns("body", 200);

            Spec.WhenWebGetRequestSent(webHostAddress + "thisaddress");

            Spec.ReceivedRequests.Count.ShouldBe(calls + 1);
            Spec.ReceivedRequests.Contains(new Request() { Method = "GET", Address = "/thisaddress" }).ShouldBe(true);
        }

    }
}