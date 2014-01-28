namespace boomerang.tests.acceptance
{
    using NUnit.Framework;

    using Rainbow.Testing.Boomerang.Host;

    using Shouldly;

    public class PostSpecs
    {
        #region Public Methods and Operators

        [Test]
        public void Should_respond_with_expectation()
        {
            Spec.GivenAServerOnSpecificPort().Post("myentity").Returns("this is my response", 201);

            Spec.WhenPostsSentTo(Spec.HostAddress + "myentity", "my data");

            Spec.ResponseText.ShouldBe("this is my response");
            Spec.StatusCode.ShouldBe("Created");
        }

        [Test]
        public void Should_register_posts_for_multiple_addresses()
        {
            Spec.GivenAServerOnSpecificPort()
                .Post("address1").Returns("response 1", 201)
                .Post("address2").Returns("response 2", 200);

            Spec.WhenPostsSentTo(Spec.HostAddress + "address1", "my data");
            Spec.ResponseText.ShouldBe("response 1");
            Spec.StatusCode.ShouldBe("Created");

            Spec.WhenPostsSentTo(Spec.HostAddress + "address2", "my data");
            Spec.ResponseText.ShouldBe("response 2");
            Spec.StatusCode.ShouldBe("OK");
        }

        [Test]
        public void Should_register_multiple_responses_for_same_address()
        {
            Spec.GivenAServerOnSpecificPort()
                .Post("address1").Returns("response 1", 201)
                .Post("address1").Returns("response 2", 200);

            Spec.WhenPostsSentTo(Spec.HostAddress + "address1", "my data");
            Spec.ResponseText.ShouldBe("response 1");
            Spec.StatusCode.ShouldBe("Created");

            Spec.WhenPostsSentTo(Spec.HostAddress + "address1", "my data");
            Spec.ResponseText.ShouldBe("response 2");
            Spec.StatusCode.ShouldBe("OK");
        }
        #endregion
    }
}