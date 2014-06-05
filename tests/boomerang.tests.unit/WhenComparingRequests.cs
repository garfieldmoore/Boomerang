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
        public void Different_body_should_unequal_with_missing_body()
        {
            var request = new Request() { Address = "Address1", Method = "put", Body = "body" };
            var request2 = new Request() { Address = "Address1", Method = "PUT" };

            request.Equals(request2).ShouldBe(false);
            request.GetHashCode().ShouldNotBe(request2.GetHashCode());
        }

        [Test]
        public void Null_body_should_equal_empty_body()
        {
            var request = new Request() { Address = "Address1", Method = "put", Body = "" };
            var request2 = new Request() { Address = "Address1", Method = "PUT", Body = null };

            request.Equals(request2).ShouldBe(true);
            request.GetHashCode().ShouldBe(request2.GetHashCode());
        }

        [Test]
        public void Null_other_body_should_equal_empty_body()
        {
            var request = new Request() { Address = "Address1", Method = "put", Body = null };
            var request2 = new Request() { Address = "Address1", Method = "PUT", Body = "" };

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
    }
}