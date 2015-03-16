namespace boomerang.tests.acceptance
{
    using NUnit.Framework;

    using Rainbow.Testing.Boomerang.Host;

    using Shouldly;

    public class PutSpecs
    {
        #region Public Methods and Operators

        [Test]
        public void Should_return_with_expected_status_code_and_response()
        {
            Spec.GivenAServerOnSpecificPort().Put("address").Returns("data updated response", 200);

            Spec.WhenPutSentTo(Spec.HostAddress + "address", "data");

            Spec.ResponseText.ShouldBe("data updated response");
            Spec.StatusCode.ShouldBe("OK");

            Spec.StopServer();

        }

        [Test]
        public void Should_register_posts_for_multiple_addresses()
        {
            Spec.GivenAServerOnSpecificPort()
                .Put("address1").Returns("response 1", 201)
                .Put("address2").Returns("response 2", 200);

            Spec.WhenPutSentTo(Spec.HostAddress + "address1", "my data");
            Spec.ResponseText.ShouldBe("response 1");
            Spec.StatusCode.ShouldBe("Created");

            Spec.WhenPutSentTo(Spec.HostAddress + "address2", "my data");
            Spec.ResponseText.ShouldBe("response 2");
            Spec.StatusCode.ShouldBe("OK");

            Spec.StopServer();

        }

        [Test]
        public void Should_register_multiple_responses_for_same_address()
        {
            Spec.GivenAServerOnSpecificPort()
                .Put("address1").Returns("response 1", 201)
                .Put("address1").Returns("response 2", 200);

            Spec.WhenPutSentTo(Spec.HostAddress + "address1", "my data");
            Spec.ResponseText.ShouldBe("response 1");
            Spec.StatusCode.ShouldBe("Created");

            Spec.WhenPutSentTo(Spec.HostAddress + "address1", "my data");
            Spec.ResponseText.ShouldBe("response 2");
            Spec.StatusCode.ShouldBe("OK");

            Spec.StopServer();

        }
        #endregion
    }
}