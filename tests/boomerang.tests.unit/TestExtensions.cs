namespace boomerang.tests.unit
{
    using System;
    using System.Linq.Expressions;

    using Rainbow.Testing.Boomerang.Host;

    using Shouldly;

    public static class TestExtensions
    {
        public static void ThenShouldContainRequestWithAddress(this BoomarangImpl target, string address)
        {
            target.Registrations.ShouldContain(x => x.Address == address);
        }

        public static void ThenShouldContainResponse(this BoomarangImpl target, Expression<Func<RequestResponse, bool>> predicate)
        {
            target.Registrations.ShouldContain(predicate);
        }

        public static void ThenShouldHaveRegisteredNumberOfResponses(this BoomarangImpl target, int count)
        {
            target.Registrations.Count.ShouldBe(count);
        }

        public static void ThenShouldHaveRegisteredNumberOfRequests(this BoomarangImpl target, int count)
        {
            target.Registrations.Count.ShouldBe(count);
        }

        public static void ThenShouldContainRequest(this BoomarangImpl target, Expression<Func<RequestResponse, bool>> predicate)
        {
            target.Registrations.ShouldContain(predicate);
        }

    }
}