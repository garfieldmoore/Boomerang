namespace boomerang.tests.acceptance.HostConfiguration
{
    using System.Net;

    using NUnit.Framework;

    using Rainbow.Testing.Boomerang.Host;
    using Rainbow.Testing.Boomerang.Host.HttpListenerProxy;

    using Shouldly;

    public class LamdaConfiguration
    {
        [Test]
        public void Should_configure_address()
        {
            var proxy = Boomerang.Create(x =>
                {
                    x.AtAddress("http://localhost:5600/");
                    x.UseHostBuilder(new HttpListenerFactory());
                }).Get("newtest").Returns("test 1", 201);

            proxy.Start();

            Spec.WhenGetRequestSent("http://localhost:5600/newtest");
            Spec.StatusCode.ShouldBe(HttpStatusCode.Created.ToString());
            Spec.ResponseText.ShouldBe("test 1");
            
        }
    }
}