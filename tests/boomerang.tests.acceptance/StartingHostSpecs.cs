namespace boomerang.tests.acceptance
{
    using NUnit.Framework;


    public class StartingHostSpecs
    {
        [Test]
        public void Should_start_host()
        {
            Spec.GivenADefaultServer();
        }

    }
}