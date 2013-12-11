namespace boomerang.tests.acceptance
{
    using NUnit.Framework;

    using Rainbow.Testing.Boomerang.Host;

    using Shouldly;

    public class DeleteSpecs
    {
        private string webHostAddress = "http://rainbow.co.uk/";

        [Test]
        public void Should_respond_with_expectation()
        {
            Spec.GivenADefaultServer().Delete("resourceAddress").Returns("202 (Accepted)", 202);

            Spec.WhenDeleteSentTo(Spec.HostAddress + "resourceAddress");

            Spec.ResponseText.ShouldBe("202 (Accepted)");
            Spec.StatusCode.ShouldBe("Accepted");

        }

    }
}