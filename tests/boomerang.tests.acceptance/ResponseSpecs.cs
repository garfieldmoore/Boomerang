namespace boomerang.tests.acceptance
{
    using System.Collections.Generic;

    using NUnit.Framework;

    using Rainbow.Testing.Boomerang.Host;

    using Shouldly;

    public class ResponseSpecs
    {
        #region Public Methods and Operators

        [Test]
        public void Should_set_headers_when_specified()
        {
            var headers = new Dictionary<string, string>();
            headers.Add("content-type", "application/json");

            Spec.GivenADefaultServer().Get("address").Returns("body", 200, headers);

            Spec.WhenGetRequestSent(Spec.HostAddress + "address");

            string header;
            Spec.ResponseHeaders.TryGetValue("Content-Type", out header).ShouldBe(true);
            header.ShouldBe("application/json");
        }

        #endregion
    }
}