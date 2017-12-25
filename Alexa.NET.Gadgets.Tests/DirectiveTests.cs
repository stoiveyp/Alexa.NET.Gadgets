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
            var actual = new StartInputHandlerDirective{MaximumHistoryLength = 512,TimeoutMilliseconds = 5000};
            Assert.True(Utility.CompareJson(actual, "StartInputHandlerDirective.json"));
        }
    }
}
