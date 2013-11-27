namespace boomerang.tests.acceptance
{
    using NUnit.Framework;

    using Rainbow.Testing.Boomerang.Host;

    using Shouldly;

    public class DeleteSpecs
    {
        private string webHostAddress = "http://localhost:5200/";

        [Test]
        public void Should_respond_with_expectation()
        {
            Spec.GivenADefaultServer().Delete("resourceAddress").Returns("202 (Accepted)", 202);

            Spec.WhenDeleteSentTo(webHostAddress + "resourceAddress");

            Spec.ResponseText.ShouldBe("202 (Accepted)");
            Spec.StatusCode.ShouldBe("Accepted");

        }

    }
}