using System.Collections.Generic;
using Alexa.NET.Gadgets.GameEngine;
using Xunit;

namespace Alexa.NET.Gadgets.Tests
{
    public class DirectiveTests
    {
        [Fact]
        public void StopInputHandlerDirectiveSerializesProperly()
        {
            var actual = new StopInputHandlerDirective
            {
                OriginatingRequestId = "amzn1.echo-api.request.406fbc75-8bf8-4077-a73d-519f53d172a4"
            };

            Assert.True(Utility.CompareJson(actual,"StopInputHandlerDirective.json"));
        }

        [Fact]
        public void GadgetEventSerializersProperly()
        {
            var actual = new GadgetEvent
            {
                Meets = new List<string> { "a recognizer", "a different recognizer" },
                Fails = new List<string> { "some other recognizer" },
                Reports = GadgetEventReportType.History,
                EndInputHandler = true,
                MaximumInvocations = 1,
                TriggerTimeMilliseconds = 1000
            };

            Assert.True(Utility.CompareJson(actual,"GadgetEvent.json"));
        }

        [Fact]
        public void StartInputHandlerDirectiveSerializesProperly()
        {
            var actual = new StartInputHandlerDirective{MaximumHistoryLength = 512,TimeoutMilliseconds = 5000};
            Assert.True(Utility.CompareJson(actual, "StartInputHandlerDirective.json"));
        }
    }
}
