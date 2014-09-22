namespace boomerang.tests.acceptance.HostConfiguration
{
    using System.Net;

    using NUnit.Framework;

    using Rainbow.Testing.Boomerang.Host;

    using Shouldly;

    public class LamdaConfiguration
    {
        [Test,Ignore]
        public void Should_configure_port()
        {
            Boomerang.Initialize(new DefaultConfigurationFactory());
            var proxy = Boomerang.Create(x=>x.OnPort(5200)).Get("test").Returns("test 1", 201);

            proxy.Start();

            Spec.WhenGetRequestSent("http://localhost:5200/test");
            Spec.StatusCode.ShouldBe(HttpStatusCode.Created.ToString());
            Spec.ResponseText.ShouldBe("test 1");

            Boomerang.Initialize(new DefaultConfigurationFactory());

        }
    
    }
}