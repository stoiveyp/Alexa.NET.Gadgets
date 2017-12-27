using System.Collections.Generic;
using Alexa.NET.Gadgets.GadgetController;
using Alexa.NET.Gadgets.GameEngine;
using Alexa.NET.Gadgets.GameEngine.Directives;
using Xunit;

namespace Alexa.NET.Gadgets.Tests
{
    public class DirectiveTests
    {
        [Fact]
        public void SetLightDirectiveSerializesProperly()
        {
            var setLight = new SetLightDirective
            {
                TargetGadgets = new List<string> { "gadgetId1", "gadgetId2" },
                Parameters = new SetLightParameter
                {
                    TriggerEvent = TriggerEvent.None,
                    TriggerEventTimeMilliseconds = 0,
                    Animations = new List<SetLightAnimation>{new SetLightAnimation
                    {
                        Repeat = 1,
                        TargetLights = new List<int> { 1 },
                        Sequence = new List<AnimationSegment>
                                {
                                    new AnimationSegment
                                    {
                                        Blend=false,
                                        DurationMilliseconds = 10000,
                                        Color="0000FF"
                                    }
                                }
                    }
                }

                }
            };

            Assert.True(Utility.CompareJson(setLight, "SetLightDirective.json"));
        }

        [Fact]
        public void StopInputHandlerDirectiveSerializesProperly()
        {
            var actual = new StopInputHandlerDirective
            {
                OriginatingRequestId = "amzn1.echo-api.request.406fbc75-8bf8-4077-a73d-519f53d172a4"
            };

            Assert.True(Utility.CompareJson(actual, "StopInputHandlerDirective.json"));
        }

        [Fact]
        public void GadgetEventSerializersProperly()
        {
            var actual = new InputHandlerEvent
            {
                Meets = new List<string> { "a recognizer", "a different recognizer" },
                Fails = new List<string> { "some other recognizer" },
                Reports = GadgetEventReportType.History,
                EndInputHandler = true,
                MaximumInvocations = 1,
                TriggerTimeMilliseconds = 1000
            };

            Assert.True(Utility.CompareJson(actual, "InputHandlerEvent.json"));
        }

        [Fact]
        public void StartInputHandlerDirectiveSerializesProperly()
        {
            var actual = new StartInputHandlerDirective { MaximumHistoryLength = 512, TimeoutMilliseconds = 5000 };
            Assert.True(Utility.CompareJson(actual, "StartInputHandlerDirective.json"));
        }
    }
}
