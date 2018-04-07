using System.Linq;
using System.Collections.Generic;
using Alexa.NET.Gadgets.GameEngine;
using Alexa.NET.Gadgets.GameEngine.Requests;
using Alexa.NET.Request;
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
        public void CommunityExampleDeserializes()
        {
            var actual = Utility.ExampleFileContent<SkillRequest>("CommunityExample.json");
            var request = actual.Request as InputHandlerEventRequest;
            request.TryRollCallResult(out Dictionary<string, string> results, "first", "second");
            Assert.NotNull(results);
            Assert.Equal("amzn1.ask.gadget.05RPH",results["first"]);
            Assert.Equal("amzn1.ask.gadget.05RPH7PJG",results["second"]);
        }

        [Fact]
        public void InputHandlerEventRequestDeserializesCorrectly()
        {
            var actual = Utility.ExampleFileContent<SkillRequest>("InputHandlerEventRequest.json");
            var request = actual.Request;
            Assert.IsType<InputHandlerEventRequest>(request);

            var inputHandlerRequest = (InputHandlerEventRequest)request;
            inputHandlerRequest.OriginatingRequestId = "amzn1.echo-api.request.406fbc75-8bf8-4077-a73d-519f53d172a4";

            var gadgetEvent = inputHandlerRequest.Events.First();
            Assert.Equal("myEventName", gadgetEvent.Name);

            var inputEvent = gadgetEvent.InputEvents.First();
            Assert.Equal("someGadgetId1", inputEvent.GadgetId);
            Assert.Equal(ButtonAction.Down, inputEvent.Action);
            Assert.Equal("FF0000", inputEvent.Color);
            Assert.Equal(ButtonFeature.Press, inputEvent.Feature);
        }
    }
}
