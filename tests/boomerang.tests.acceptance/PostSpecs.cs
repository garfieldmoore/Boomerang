namespace boomerang.tests.acceptance
{
    using NUnit.Framework;

    using Rainbow.Testing.Boomerang.Host;

    using Shouldly;

    public class PostSpecs
    {
        private string webHostAddress = "http://localhost:5200/";

        [Test]
        public void Should_respond_with_expectation()
        {
            Spec.GivenADefaultServer().Post("myentity", "values").Returns(200, "this is my response");

            Spec.WhenPostsSentTo(webHostAddress + "myentity", "my data");

            Spec.ResponseText.ShouldBe("this is my response");
        }

    }
}