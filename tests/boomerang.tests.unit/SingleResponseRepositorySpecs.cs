using System.Collections.Generic;
using FizzWare.NBuilder;
using NUnit.Framework;
using Rainbow.Testing.Boomerang.Host;
using Shouldly;

namespace boomerang.tests.unit
{
    public class SingleResponseRepositorySpecs
    {
        private SingleResponseRepository _repository;

        [Test]
        public void Contains_returns_false_if_request_not_added()
        {
            GivenASingleResponseRepository();
            _repository.RegisteredAddresses.ContainsKey(new Request()).ShouldBe(false);
        }

        [Test]
        public void Should_return_response()
        {
            var repository = new SingleResponseRepository();

            var request = Builder<Request>.CreateNew().Build();
            repository.AddAddress(request);

            repository.RegisteredAddresses.ContainsKey(request).ShouldBe(true);
        }

        [Test]
        public void Count_returns_number_of_added_requests()
        {
            GivenASingleResponseRepository();
            _repository.AddAddress(new Request());

            _repository.RegisteredAddresses.Count.ShouldBe(1);
        }

        [Test]
        public void Should_return_two_when_two_requests_added()
        {
            GivenASingleResponseRepository();

            _repository.AddAddress(Builder<Request>.CreateNew().Build());
            _repository.AddAddress(Builder<Request>.CreateNew().With(x => x.Method = "GET").Build());

            _repository.RegisteredAddresses.Count.ShouldBe(2);
        }

        [Test]
        public void AddResponse_adds_response_to_address()
        {
            GivenASingleResponseRepository();

            var request = Builder<Request>.CreateNew().Build();
            _repository.AddAddress(request);

            _repository.AddResponse("my body", 202);

            _repository.RegisteredAddresses.Contains(new KeyValuePair<Request, Response>(request,
                new Response() { Body = "my body", StatusCode = 202 }));
        }

        [Test]
        public void Adding_same_addresses_again_does_not_add_another_adress()
        {
            GivenASingleResponseRepository();

            var request = Builder<Request>.CreateNew().Build();
            _repository.AddAddress(request);
            _repository.AddAddress(request);

            _repository.RegisteredAddresses.Count.ShouldBe(1);
        }

        [Test]
        public void Adding_multiple_responses_replaces_previous()
        {
            GivenASingleResponseRepository();
            var request = Builder<Request>.CreateNew().Build();
            _repository.AddAddress(request);

            _repository.AddResponse("r1", 404);
            _repository.AddResponse("r2", 201);

            Response response;
            _repository.RegisteredAddresses.TryGetValue(request, out response);

            response.Body.ShouldBe("r2");
            response.StatusCode.ShouldBe(201);
        }

        [Test]
        public void AddResponse_replace_previous_add_attempt_when_adding_same_address_multiple_times()
        {
            GivenASingleResponseRepository();
            var request1 = Builder<Request>.CreateNew().With(x => x.Address = "address2").Build();
            var request2 = Builder<Request>.CreateNew().With(x => x.Method = "new").Build();
            var response = Builder<Response>.CreateNew().Build();

            _repository.AddAddress(request1);
            _repository.AddAddress(request2);
            _repository.AddAddress(request1);

            _repository.AddResponse(response.Body, response.StatusCode);

            Response registeredResponse;
            _repository.RegisteredAddresses.TryGetValue(request1, out registeredResponse);

            registeredResponse.Body.ShouldBe(response.Body);
            registeredResponse.StatusCode.ShouldBe(response.StatusCode);
        }

        [Test]
        public void AddResponse_with_headers_adds_headers()
        {
            GivenASingleResponseRepository();

            var request = Builder<Request>.CreateNew().Build();

            _repository.AddAddress(request);
            var dictionary = new Dictionary<string, string>();
            dictionary.Add("k1", "v1");
            _repository.AddResponse("r1", 200, dictionary);

            Response response;
            _repository.RegisteredAddresses.TryGetValue(request, out response);

            response.Headers.Count.ShouldBe(1);
        }

        [Test]
        public void GetNextResponse_returns_response_for_address()
        {
            GivenASingleResponseRepository();

            var request = Builder<Request>.CreateNew().Build();
            _repository.AddAddress(request);
            _repository.AddResponse("b1", 201);

            var response = _repository.GetNextResponseFor(request.Method, request.Address);

            response.StatusCode.ShouldBe(201);
            response.Body.ShouldBe("b1");
        }

        [Test]
        public void GetNextResponse_invoked_multiple_times_returns_same_response()
        {
            GivenASingleResponseRepository();

            var request = Builder<Request>.CreateNew().Build();
            _repository.AddAddress(request);
            _repository.AddResponse("b1", 201);

            var response = _repository.GetNextResponseFor(request.Method, request.Address);
            response = null;
            response = _repository.GetNextResponseFor(request.Method, request.Address);

            response.StatusCode.ShouldBe(201);
            response.Body.ShouldBe("b1");
        }

        private void GivenASingleResponseRepository()
        {
            _repository = new SingleResponseRepository();
        }
    }
}