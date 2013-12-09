
namespace boomerang.tests.acceptance
{
    using System.Net;

    using NUnit.Framework;
    using Rainbow.Testing.Boomerang.Host;
    using Shouldly;

    public class GetReponseSpecs
    {
        private string webHostAddress = "http://localhost:5200/";

        [Test]
        public void Should_allow_expectations_on_server_base_address()
        {
            Spec.GivenADefaultServer().Get("").Returns("Boomerang interception", 200);

            Spec.WhenGetRequestSent(webHostAddress);

            Spec.ResponseText.ShouldBe("Boomerang interception");
            Spec.StatusCode.ShouldBe("OK");
        }

        [Test]
        public void Should_use_address_body_and_statusCode()
        {
            Spec.GivenADefaultServer().Get("address1").Returns("body1", 200);

            Spec.WhenGetRequestSent(webHostAddress + "address1");

            Spec.StatusCode.ShouldBe(HttpStatusCode.OK.ToString());
            Spec.ResponseText.ShouldBe("body1");
        }

        [Test]
        public void Should_use_correct_responses_for_multiple_addresss()
        {
            Spec.GivenADefaultServer().Get("address1").Returns("body1", 200).Get("address2").Returns("body2", 401);

            Spec.WhenGetRequestSent(webHostAddress + "address1");

            Spec.StatusCode.ShouldBe("OK");
            Spec.ResponseText.ShouldBe("body1");

            Spec.WhenGetRequestSent(webHostAddress + "address2");

            Spec.ResponseText.ShouldBe("body2");
            Spec.StatusCode.ShouldBe(HttpStatusCode.Unauthorized.ToString());
        }

        [Test]
        public void Should_register_different_responses_against_same_address()
        {
            Spec.GivenADefaultServer()
                .Get("address1").Returns("body1", 200)
                .Get("address1").Returns("body2", 201);

            Spec.WhenGetRequestSent(webHostAddress + "address1");

            Spec.ResponseText.ShouldBe("body1");
            Spec.StatusCode.ShouldBe(HttpStatusCode.OK.ToString());

            Spec.WhenGetRequestSent(webHostAddress + "address1");

            Spec.ResponseText.ShouldBe("body2");
            Spec.StatusCode.ShouldBe(HttpStatusCode.Created.ToString());

        }

    }
}
