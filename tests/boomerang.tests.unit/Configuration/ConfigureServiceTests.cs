namespace boomerang.tests.unit.Configuration
{
    using NUnit.Framework;

    using Rainbow.Testing.Boomerang.Host;

    public class ConfigureServiceTests
    {
        [Test]
        public void Should_do_x()
        {
            Boomerang.Start(x => x);
        }

    }
}