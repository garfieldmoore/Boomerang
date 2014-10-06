namespace boomerang.tests.unit
{
    using FizzWare.NBuilder;

    using NUnit.Framework;

    using Rainbow.Testing.Boomerang.Host;

    using Shouldly;

    public class WhenComparingRequests
    {
        [Test]
        public void Same_method_and_address_means_equality()
        {
            var request = Builder<Request>.CreateNew().Build();
            var request2 = Builder<Request>.CreateNew().Build();

            request.Equals(request2).ShouldBe(true);
            request.GetHashCode().ShouldBe(request2.GetHashCode());
        }

        [Test]
        public void Different_address_cases_should_be_equal()
        {
            var request = new Request() { Address = "Address1", Method = "PUT" };
            var request2 = new Request() { Address = "ADDRESS1", Method = "PUT" };

            request.Equals(request2).ShouldBe(true);
            request.GetHashCode().ShouldBe(request2.GetHashCode());
        }

        [Test]
        public void Different_method_cases_should_be_equal()
        {
            var request = new Request() { Address = "Address1", Method = "put" };
            var request2 = new Request() { Address = "Address1", Method = "PUT" };

            request.Equals(request2).ShouldBe(true);
            request.GetHashCode().ShouldBe(request2.GetHashCode());
        }

        [Test]
        public void Relative_address_should_start_with_forward_slash()
        {
            var request = new Request();

            request.Address = "address";

            request.Address.ShouldBe("/address");
        }

        [Test]
        public void Different_address_should_not_be_equal()
        {
            var request1 = new Request() { Address = "a1", Method = "GET" };
            var request2 = new Request() { Address = "a2", Method = "GET" };

            request1.Equals(request2).ShouldBe(false);
            request1.GetHashCode().ShouldNotBe(request2.GetHashCode());
        }

        [Test]
        public void Different_method_should_not_be_equal()
        {
            var request1 = new Request() { Address = "a1", Method = "GET" };
            var request2 = new Request() { Address = "a1", Method = "PUT" };

            request1.Equals(request2).ShouldBe(false);
            request1.GetHashCode().ShouldNotBe(request2.GetHashCode());
        }
    }
}