using System;
using Alexa.NET.Response.Directive;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Alexa.NET.Gadgets.Tests
{
    public class DirectiveTests
    {
        [Fact]
        public void StartInputHandlerDirectiveSerializesProperly()
        {
            var directive = new StartInputHandlerDirective{MaximumHistoryLength = 512,TimeoutMilliseconds = 5000};
            var actual = JObject.FromObject(directive);
            Assert.True(Utility.CompareJson(actual, "StartInputHandlerDirective.json"));
        }
    }
}
