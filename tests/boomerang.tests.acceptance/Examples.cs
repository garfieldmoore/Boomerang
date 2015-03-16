using Shouldly;

namespace boomerang.tests.acceptance
{
    using NUnit.Framework;

    using Rainbow.Testing.Boomerang.Host;

    public class Examples
    {       
        [Test]
        [Ignore]
        public void Get_register_multiple_responses()
        {
            Boomerang.Create(x => x.AtAddress(Spec.HostAddress))
                     .Get(Spec.HostAddress+ "anaddress")
                     .Returns("response body", 200)
                     .Get("anotheraddress")
                     .Returns("another response body", 201);

            Spec.WhenGetRequestSent("anaddress");
            Spec.StatusCode.ShouldBe("ok");
        }
     }
}