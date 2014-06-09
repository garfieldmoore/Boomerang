namespace boomerang.tests.acceptance
{
    using NUnit.Framework;

    using Rainbow.Testing.Boomerang.Host;

    using Shouldly;

    public class PerformanceTests
    {
//        [Test, Repeat(100)]
//        [Ignore]
//        public void Should_get_100_times()
//        {
//            Boomerang.Server(5100).Get("address").Returns("test", 200);
//
//            Spec.WhenGetRequestSent("http://localhost:5100/address");
//            Spec.ResponseText.ShouldBe("test");
//            Spec.StatusCode.ShouldBe("OK");
//        }
//
//        [Test, Repeat(100)]
//        [Ignore]
//        public void Should_get2_100_times()
//        {
//            Boomerang.Server(5100).Get("address").Returns("test1", 200);
//
//            Spec.WhenGetRequestSent("http://localhost:5100/address");
//            Spec.ResponseText.ShouldBe("test1");
//            Spec.StatusCode.ShouldBe("OK");
//        }

        [Test]
        public void Should_test1()
        {
            Boomerang.Server(5100).ClearReceivedRequests();
            Boomerang.Server(5100).Get("address").Returns("test1", 200).Post("address/owner").Returns("me",201).Put("address/owner/me1").Returns("address/owner/me/updated1", 200);

            Spec.WhenGetRequestSent("http://localhost:5100/address");
            Spec.ResponseText.ShouldBe("test1");
            Spec.StatusCode.ShouldBe("OK");
        }
        [Test]
        public void Should_test2()
        {
            Boomerang.Server(5100).ClearReceivedRequests();
            Boomerang.Server(5100).Get("address").Returns("test2", 200).Post("address/owner").Returns("me", 201).Put("address/owner/me2").Returns("address/owner/me/updated2", 200);

            Spec.WhenGetRequestSent("http://localhost:5100/address");
            Spec.ResponseText.ShouldBe("test2");
            Spec.StatusCode.ShouldBe("OK");
        }

        [Test]
        public void Should_test3()
        {
            Boomerang.Server(5100).ClearReceivedRequests();
            Boomerang.Server(5100).Get("address").Returns("test3", 200).Post("address/owner").Returns("me", 201).Put("address/owner/me3").Returns("address/owner/me/updated3", 200);

            Spec.WhenGetRequestSent("http://localhost:5100/address");
            Spec.ResponseText.ShouldBe("test3");
            Spec.StatusCode.ShouldBe("OK");
        }



    }
}