
namespace boomerang.tests.acceptance
{
    using System.Net;

    using NUnit.Framework;
    using Rainbow.Testing.Boomerang.Host;
    using Shouldly;

    public class ExampleIInterfaceSpecs
    {
        private string status;

        private string responseFromServer;

        private string webHostAddress = "http://localhost:5200/";

        [Test]
        public void Server_interface()
        {
            Spec.GivenADefaultServer().Get("any").Returns("Boomerang interception", 200);

            Spec.WhenWebGetRequestSent(webHostAddress+"any");

            Spec.ResponseText.ShouldBe("Boomerang interception");
            Spec.StatusCode.ShouldBe("OK");
        }

        [Test]
        public void Simple_uniform_interface()
        {
            Spec.GivenADefaultServer().Get("address1").Returns("body1", 200);

            Spec.WhenWebGetRequestSent(webHostAddress + "address1");

            Spec.StatusCode.ShouldBe(HttpStatusCode.OK.ToString());
            Spec.ResponseText.ShouldBe("body1");
        }

        [Test]
        public void Should_be_able_to_specify_multiple_addresses()
        {
            Spec.GivenADefaultServer().Get("address1").Returns("body1", 200).Get("address2").Returns("body2", 401);

            Spec.WhenWebGetRequestSent(webHostAddress + "address1");

            Spec.StatusCode.ShouldBe("OK");
            Spec.ResponseText.ShouldBe("body1");

            Spec.WhenWebGetRequestSent(webHostAddress + "address2");

            Spec.ResponseText.ShouldBe("body2");
            Spec.StatusCode.ShouldBe(HttpStatusCode.Unauthorized.ToString());

        }

    }
}
