using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Runtime.Remoting.Messaging;
using FizzWare.NBuilder;
using Newtonsoft.Json;
using NUnit.Framework;
using Rainbow.Testing.Boomerang.Host;
using RestSharp.Deserializers;
using RestSharp.Serializers;
using Shouldly;

namespace boomerang.tests.acceptance
{
    [TestFixture]
    public class ComplexTypesSerialisationSpecs
    {
        [Test]
        public void Should_work_with_complex_object()
        {
            var coffee = Builder<Coffee>.CreateNew().Build();
            var headers = new Dictionary<string, string>();
            headers.Add("Content-Type", "application/json");
            string serializeObject = JsonConvert.SerializeObject(coffee);

            Spec.GivenAServerOnSpecificPort().Get("address").Returns(serializeObject, 200, headers);

            Spec.WhenGetRequestSentOf<Coffee>(Spec.HostAddress + "address");
            Spec.Data.ShouldBeTypeOf<Coffee>();
            ((Coffee)Spec.Data).Name.ShouldBe(coffee.Name);
        }

        [Test]
        public void Should_work_with_list_complex_objects()
        {
            var coffee = Builder<Coffee>.CreateListOfSize(2).Build() as List<Coffee>;
            var headers = new Dictionary<string, string>();

            headers.Add("Content-Type", "application/json");
            string serializeObject = JsonConvert.SerializeObject(coffee);

            Spec.GivenAServerOnSpecificPort().Get("address").Returns(serializeObject, 200, headers);

            Spec.WhenGetRequestSentOf<List<Coffee>>(Spec.HostAddress + "address");
            Spec.StopServer();
            Spec.Data.ShouldBeTypeOf<List<Coffee>>();
            ((List<Coffee>)Spec.Data)[0].Name.ShouldBe(coffee[0].Name);
        }
    }
}