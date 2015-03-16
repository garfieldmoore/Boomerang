namespace boomerang.tests.unit
{

    using NSubstitute;

    using NUnit.Framework;

    using Rainbow.Testing.Boomerang.Host;

    using Shouldly;

    public class GivenARequest
    {
        private int wasCalled;
        private BoomarangImpl _boomerang;
        private IMasqarade _masqarade;
        private HostSettings _hostSettings;

        [Test]
        public void Should_raise_event()
        {
            
            GivenAWebProxy();

            wasCalled = 0;
            _boomerang.OnReceivedRequest += boomerang_OnReceivedRequest;

            _boomerang.Start();

            _masqarade.BeforeRequest += Raise.EventWith(_masqarade, new ProxyRequestEventArgs() { Method = "GET", RelativePath = "thisaddress" });

            wasCalled.ShouldBe(1);
        }

        private void GivenAWebProxy()
        {
            _masqarade = Substitute.For<IMasqarade>();
            _hostSettings = new HostSettings();
            _hostSettings.Prefixes.Add("http://localhost");
            _boomerang = new BoomarangImpl(_masqarade, _hostSettings);
        }

        [Test]
        public void Should_not_throw_exception_when_no_subscribers()
        {
            GivenAWebProxy();

            _boomerang.Start();

            _masqarade.BeforeRequest += Raise.EventWith(_masqarade, new ProxyRequestEventArgs() { Method = "GET", RelativePath = "thisaddress" });
        }

        private void boomerang_OnReceivedRequest(object sender, ProxyRequestEventArgs e)
        {
            wasCalled++;
        }
    }
}