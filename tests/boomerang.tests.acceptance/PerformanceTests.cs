namespace boomerang.tests.acceptance
{
    using NUnit.Framework;

    using Rainbow.Testing.Boomerang.Host;

    using Shouldly;

    public class PerformanceTests
    {
        [Test, Repeat(100)]
        [Ignore]
        public void Should_get_100_times()
        {
            Boomerang.Server(5100).Get("address").Returns("test", 200);

            Spec.WhenGetRequestSent("http://localhost:5100/address");
            Spec.ResponseText.ShouldBe("test");
            Spec.StatusCode.ShouldBe("OK");
        }

        [Test, Repeat(100)]
        [Ignore]
        public void Should_get2_100_times()
        {
            Boomerang.Server(5100).Get("address").Returns("test1", 200);

            Spec.WhenGetRequestSent("http://localhost:5100/address");
            Spec.ResponseText.ShouldBe("test1");
            Spec.StatusCode.ShouldBe("OK");
        }

    }
}