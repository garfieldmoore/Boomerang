using NUnit.Framework;
using Rainbow.Testing.Boomerang.Extensions;
using Rainbow.Testing.Boomerang.Host;
using Shouldly;

namespace boomerang.tests.acceptance.Repeat
{
    public class RepeatSpecs
    {
        [Test]
        public void Should_repeat_with_functions()
        {
            Spec.GivenAServerOnSpecificPort()
                .Repeat((x) => x.Get("test").Returns("test2", 201), 2);

            Spec.WhenGetRequestSent(Spec.HostAddress + "test");
            Spec.ResponseText.ShouldBe("test2");
            Spec.StatusCode.ShouldBe("Created");

//            Spec.WhenGetRequestSent(Spec.HostAddress + "test");
//            Spec.ResponseText.ShouldBe("test2");
//            Spec.StatusCode.ShouldBe("Created");
        }
    }
}