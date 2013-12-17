﻿namespace boomerang.tests.acceptance
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

        #endregion
    }
}