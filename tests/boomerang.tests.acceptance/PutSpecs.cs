namespace boomerang.tests.acceptance
{
    using NUnit.Framework;

    using Rainbow.Testing.Boomerang.Host;

    using Shouldly;

    public class PutSpecs
    {
        private string webHostAddress = "http://localhost:5200/";

        [Test]
        public void Should_return_with_expected_status_code_and_response()
        {
            Spec.GivenADefaultServer().Put("address").Returns("data updated response", 200);

            Spec.WhenPutSentTo(webHostAddress + "address", "data");

            Spec.ResponseText.ShouldBe("data updated response");
            Spec.StatusCode.ShouldBe("OK");
        }
    }
}