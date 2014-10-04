using NUnit.Framework;
using Rainbow.Testing.Boomerang.Host;
using Shouldly;

namespace boomerang.tests.acceptance.SecureHttp
{
    public class SecureHttpsSpecs
    {
        [Test]
        public void ShouldInitialiseSecureHttpsServer()
        {
            Spec.HostAddress = "https://localhost:5100/";
            string gotItSecurely = "got it securely";
            Spec.GivenAServerOnSpecificPort().Get("sensitiveEndpoint").Returns(gotItSecurely, 200);
            
            Spec.WhenGetRequestSent(Spec.HostAddress + "sensitiveEndpoint");

            Spec.StatusCode.ShouldBe("OK");
            Spec.ResponseText.ShouldBe(gotItSecurely);
        }

    }

    public static class ServerExtensions
    {
        public static IBoomerang AsSecureServer(this IBoomerang host)
        {
            return host;
        }
    }
}