namespace boomerang.tests.unit
{
    using System.Collections.Generic;

    using Rainbow.Testing.Boomerang.Host;

    using Shouldly;

    internal static class RequestRepositoryExtensions
    {
        /// <summary>
        /// Returns the responses registered for all addresses
        /// </summary>
        /// <returns>Returns all responses for all address</returns>
        public static IEnumerable<Queue<Response>> Requests(this ResponseRepository target)
        {
            return target.RequestResponseRegistrations.Values;
        }

        /// <summary>
        /// Determines if a request has been registered
        /// </summary>
        /// <param name="request">The request to check</param>
        /// <returns>True if the method and uri has been configured, false otherwise</returns>
        public static bool Contains(this ResponseRepository target,Request request)
        {
            return target.RequestResponseRegistrations.ContainsKey(request);
        }

        /// <summary>
        /// Returns the number of registered uri's
        /// </summary>
        /// <returns>Number of registered uri's</returns>
        public static int GetCount(this ResponseRepository target)
        {
            return target.RequestResponseRegistrations.Count;
        }

        /// <summary>
        /// Return all responses for a request
        /// </summary>
        /// <param name="request">The request to return responses for</param>
        /// <param name="req">Set this to the configured responses if they exist for the request, otherwise null</param>
        /// <returns>True if there were responses for the request</returns>
        public static bool GetAllResponsesFor(this ResponseRepository target, Request request, out Queue<Response> req)
        {
            return target.RequestResponseRegistrations.TryGetValue(request, out req);
        }
    }

    public static class TestExtensions
    {
        internal static void ThenShouldContainRequestWithAddress(this BoomarangImpl target, string address)
        {
            ((ResponseRepository)RequestHandlers.Handler).Contains(new Request() { Address = address, Method = "GET" }).ShouldBe(true);
        }

        internal static void ThenShouldHaveRegisteredNumberOfResponses(this BoomarangImpl target, int count)
        {
            var numberOfResponses = 0;

            foreach (var requestResponseRegistration in ((ResponseRepository)RequestHandlers.Handler).Requests())
            {
                numberOfResponses += requestResponseRegistration.Count;
            }

            numberOfResponses.ShouldBe(count);
        }

        internal static void ThenShouldHaveRegisteredNumberOfRequests(this BoomarangImpl target, int count)
        {
            ((ResponseRepository)RequestHandlers.Handler).GetCount().ShouldBe(count);
        }

        internal static void ThenShouldContainRequest(this BoomarangImpl target, string method, string address)
        {
            var contains = ((ResponseRepository)RequestHandlers.Handler).Contains(new Request() { Address = address, Method = method });

            contains.ShouldBe(true);
        }

        internal static void ThenShouldContainPostResponse(this BoomarangImpl target, string address, string responseBody)
        {
            Queue<Response> req;
            ((ResponseRepository)RequestHandlers.Handler).GetAllResponsesFor(new Request() { Address = address, Method = "POST" }, out req);

            req.ShouldNotBe(null);
            req.Count.ShouldBeGreaterThan(0);
            var res = req.Dequeue();
            res.Body.ShouldBe(responseBody);
        }

        internal static void ThenShouldContainPutResponse(this BoomarangImpl target, string address, string responseBody)
        {
            Queue<Response> req;
            ((ResponseRepository)RequestHandlers.Handler).GetAllResponsesFor(new Request() { Address = address, Method = "PUT" }, out req);

            req.ShouldNotBe(null);
            req.Count.ShouldBeGreaterThan(0);
            var res = req.Dequeue();
            res.Body.ShouldBe(responseBody);
        }
    }
}