namespace boomerang.tests.acceptance
{
    using NUnit.Framework;

    using Rainbow.Testing.Boomerang.Host;

    public class StartingHostSpecs
    {
        [Test]
        public void Should_start_host()
        {
            Spec.GivenADefaultServer();
        }
    }
}