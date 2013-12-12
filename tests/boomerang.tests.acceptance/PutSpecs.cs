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
            Spec.GivenADefaultServer().Put("address").Returns("data updated response", 200);

            Spec.WhenPutSentTo(Spec.HostAddress + "address", "data");

            Spec.ResponseText.ShouldBe("data updated response");
            Spec.StatusCode.ShouldBe("OK");
        }

        #endregion
    }
}