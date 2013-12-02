namespace boomerang.tests.unit
{
    using FizzWare.NBuilder;

    using NUnit.Framework;

    using Rainbow.Testing.Boomerang.Host;

    using Shouldly;

    public class WhenComparingRequests
    {
        [Test]
        public void Same_mehtod_and_address_means_equality()
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

 
    }
}