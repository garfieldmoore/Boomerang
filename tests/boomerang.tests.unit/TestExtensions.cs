namespace boomerang.tests.unit
{
    using System.Collections.Generic;
    using Rainbow.Testing.Boomerang.Host;
    using Shouldly;

    public static class TestExtensions
    {
        public static void ThenShouldContainRequestWithAddress(this BoomarangImpl target, string address)
        {
            target.Registrations.Contains(new Request() { Address = address, Method = "GET" }).ShouldBe(true);
        }

        public static void ThenShouldHaveRegisteredNumberOfResponses(this BoomarangImpl target, int count)
        {
            var numberOfResponses = 0;

            foreach (var requestResponseRegistration in target.Registrations.Requests())
            {
                numberOfResponses += requestResponseRegistration.Count;
            }

            numberOfResponses.ShouldBe(count);
        }

        public static void ThenShouldHaveRegisteredNumberOfRequests(this BoomarangImpl target, int count)
        {
            target.Registrations.GetCount().ShouldBe(count);
        }

        public static void ThenShouldContainRequest(this BoomarangImpl target, string method, string address)
        {
            var contains = target.Registrations.Contains(new Request() { Address = address, Method = method });

            contains.ShouldBe(true);
        }

        public static void ThenShouldContainPostResponse(this BoomarangImpl target, string address, string responseBody)
        {
            Queue<Response> req;
            target.Registrations.GetAllResponsesFor(new Request() { Address = address, Method = "POST" }, out req);

            req.ShouldNotBe(null);
            req.Count.ShouldBeGreaterThan(0);
            var res = req.Dequeue();
            res.Body.ShouldBe(responseBody);
        }

        public static void ThenShouldContainPutResponse(this BoomarangImpl target, string address, string responseBody)
        {
            Queue<Response> req;
            target.Registrations.GetAllResponsesFor(
                    new Request() { Address = address, Method = "PUT" }, out req);

            req.ShouldNotBe(null);
            req.Count.ShouldBeGreaterThan(0);
            var res = req.Dequeue();
            res.Body.ShouldBe(responseBody);
        }
    }
}