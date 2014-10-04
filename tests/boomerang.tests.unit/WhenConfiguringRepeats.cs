using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using Rainbow.Testing.Boomerang.Extensions;
using Rainbow.Testing.Boomerang.Host;
using Shouldly;

namespace boomerang.tests.unit
{
    public class WhenConfiguringRepeats
    {
        [Test]
        public void Should_register_two_resposnes()
        {
            var boom = new BoomarangImpl(Substitute.For<IMasqarade>());
            var configurator = boom.Repeat(x => x.Get("test").Returns("response", 201), 2) as BoomarangImpl;

            Queue<Response> queue;
            RequestHandlers.Handler.GetAllResponsesFor(new Request() { Address = "test", Method = "GET" }, out queue);

            queue.Count.ShouldBe(2);
        }
    }
}