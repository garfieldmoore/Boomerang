namespace boomerang.tests.acceptance
{
    using NUnit.Framework;

    using Rainbow.Testing.Boomerang.Host;

    public class PostSpecs
    {
        private string webHostAddress = "http://localhost:5200/";

        [Test]
        public void Should_respond_with_expectation()
        {
            Boomerang.Server(5100).Post(webHostAddress, "values").Returns("this is my response");
        }

    }
}