using System;
using System.Collections.Generic;
using Alexa.NET.Response.Directive;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Alexa.NET.Gadgets.Tests
{
    public class DirectiveTests
    {
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
