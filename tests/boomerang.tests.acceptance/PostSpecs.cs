using System.Collections.Generic;
using System.Linq;

namespace boomerang.tests.acceptance
{
    using NUnit.Framework;

    using Rainbow.Testing.Boomerang.Host;

    using Shouldly;

    public class PostSpecs
    {
        [Test]
        public void Should_respond_with_expectation()
        {
            Spec.GivenAServerOnSpecificPort().Post("myentity").Returns("this is my response", 201);

            Spec.WhenPostsSentTo(Spec.HostAddress + "myentity", "my data");

            Spec.ResponseText.ShouldBe("this is my response");
            Spec.StatusCode.ShouldBe("Created");
        }

        [Test]
        public void Should_respond_with_expectation_for_body()
        {
            Spec.GivenAServerOnSpecificPort().Post("myentity", "\"my body\"").Returns("this is my response", 201);

            Spec.WhenPostsSentTo(Spec.HostAddress + "myentity", "my body");

            Spec.ResponseText.ShouldBe("this is my response");
            Spec.StatusCode.ShouldBe("Created");
        }

        [Test]
        public void Should_respond_with__error_for_unregistered_body()
        {
            Spec.GivenAServerOnSpecificPort().Post("myentity", "my body").Returns("this is my response", 201);

            Spec.WhenPostsSentTo(Spec.HostAddress + "myentity", "other body");

            Spec.StatusCode.ShouldBe("BadRequest");
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

        [Test]
        public void Should_set_request_body()
        {
            Spec.GivenAServerOnSpecificPort()
                .Post("myaddress1").Returns("response 1", 201);

            Spec.WhenPostsSentTo(Spec.HostAddress + "myaddress1", "my data");

            var received = Spec.GivenAServerOnSpecificPort().GetAllReceivedRequests().Where(x => x.Method == "POST" && x.Address == "/myaddress1").ToList();

            received[0].Method.ShouldBe("POST");
            received[0].Address.ShouldBe("/myaddress1");
            received[0].Body.ToString().ShouldContain("my data");
        }
    }
}