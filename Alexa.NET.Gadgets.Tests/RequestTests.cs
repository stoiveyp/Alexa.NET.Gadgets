using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.Gadgets.GameEngine.Requests;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Xunit;

namespace Alexa.NET.Gadgets.Tests
{
    public class RequestTests
    {
        public RequestTests()
        {
            new GadgetRequestHandler().AddToRequestConverter();
        }

        [Fact]
        public void InputHandlerEventRequestDeserializesCorrectly()
        {
            var actual = Utility.ExampleFileContent<SkillRequest>("InputHandlerEventRequest.json");
        }
    }
}
