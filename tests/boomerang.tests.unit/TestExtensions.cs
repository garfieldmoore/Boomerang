namespace boomerang.tests.unit
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Rainbow.Testing.Boomerang.Host;

    using Shouldly;

    using System.Linq;

    public static class TestExtensions
    {
        public static void ThenShouldContainRequestWithAddress(this BoomarangImpl target, string address)
        {
            target.Registrations.RequestResponseRegistrations.ContainsKey(new Registration(){Address=address,Method = "GET"}).ShouldBe(true);
        }

        public static void ThenShouldHaveRegisteredNumberOfResponses(this BoomarangImpl target, int count)
        {
            var numberOfResponses = 0;

            foreach (var requestResponseRegistration in target.Registrations.RequestResponseRegistrations)
            {
                numberOfResponses += requestResponseRegistration.Value.Responses.Count;
            }
           
            numberOfResponses.ShouldBe(count);
        }

        public static void ThenShouldHaveRegisteredNumberOfRequests(this BoomarangImpl target, int count)
        {
            target.Registrations.RequestResponseRegistrations.Count.ShouldBe(count);
        }

        public static void ThenShouldContainRequest(this BoomarangImpl target, string method, string address)
        {
            target.Registrations.RequestResponseRegistrations.ContainsKey(new Registration(){Address=address,Method=method});
        }

        public static void ThenShouldContainPostResponse(this BoomarangImpl target, string address, string responseBody)
        {
            RequestResponse req;
            target.Registrations.RequestResponseRegistrations.TryGetValue(
                    new Registration() { Address = address, Method = "POST" }, out req);

            req.ShouldNotBe(null);
            req.Responses.Count.ShouldBeGreaterThan(0);
            var res = req.Responses.Dequeue();
            res.Body.ShouldBe(responseBody);
        }

        public static void ThenShouldContainPutResponse(this BoomarangImpl target, string address, string responseBody)
        {
            RequestResponse req;
            target.Registrations.RequestResponseRegistrations.TryGetValue(
                    new Registration() { Address = address, Method = "PUT" }, out req);

            req.ShouldNotBe(null);
            req.Responses.Count.ShouldBeGreaterThan(0);
            var res = req.Responses.Dequeue();
            res.Body.ShouldBe(responseBody);
        }
    }
}