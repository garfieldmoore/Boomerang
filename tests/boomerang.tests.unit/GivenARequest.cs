namespace boomerang.tests.unit
{

    using NSubstitute;

    using NUnit.Framework;

    using Rainbow.Testing.Boomerang.Host;

    using Shouldly;

    public class GivenARequest
    {
        private int wasCalled;

        [Test]
        public void Should_raise_event()
        {
            var masqarade = Substitute.For<IMasqarade>();
            var boomerang = new BoomarangImpl(masqarade);

            wasCalled = 0;
            boomerang.OnReceivedRequest += boomerang_OnReceivedRequest;

            boomerang.Start(1);

            masqarade.BeforeRequest += Raise.EventWith(masqarade, new ProxyRequestEventArgs() { Method = "GET", RelativePath = "thisaddress" });

            wasCalled.ShouldBe(1);
        }

        [Test]
        public void Should_not_throw_exception_when_no_subscribers()
        {
            var masqarade = Substitute.For<IMasqarade>();
            var boomerang = new BoomarangImpl(masqarade);

            boomerang.Start(1);

            masqarade.BeforeRequest += Raise.EventWith(masqarade, new ProxyRequestEventArgs() { Method = "GET", RelativePath = "thisaddress" });
        }

        private void boomerang_OnReceivedRequest(object sender, ProxyRequestEventArgs e)
        {
            wasCalled++;
        }
    }
}