using System;
using System.Collections.Generic;
using System.Linq;
using Alexa.NET.Request;
using Alexa.NET.Response;
using Alexa.NET.Gadgets.GadgetController;
using Alexa.NET.Gadgets.GameEngine;
using Alexa.NET.Gadgets.GameEngine.Requests;
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
                    Animations = new List<SetLightAnimation> {
                        new SetLightAnimation {
                            Repeat = 1,
                            TargetLights = new List<string> { "1" },
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
        public void SetLightDirectiveViaCreationSerializesProperly()
        {
            var setLight = SetLightDirective.Create(
                new[] { "gadgetId1", "gadgetId2" },
                SetLightParameter.Create(
                    TriggerEvent.None, 0,
                    SetLightAnimation.Create(1, new[] { "1" },
                        new AnimationSegment
                        {
                            Blend = false,
                            DurationMilliseconds = 10000,
                            Color = "0000FF"
                        }
                    )));

            Assert.True(Utility.CompareJson(setLight, "SetLightDirective.json"));
        }

        [Fact]
        public void SetLightDirectiveCreationWithNoGadgetsSerializesProperly()
        {
            var setLight = SetLightDirective.Create(
                SetLightParameter.Create(
                    SetLightAnimation.CreateSingle(
                        AnimationSegment.Create("0000FF", 10000)
                    )));

            Assert.True(Utility.CompareJson(setLight, "SetLightDirectiveBroadcast.json"));
        }

        [Fact]
        public void GadgetColorExtensionWithNoGadgets()
        {
            var response = new SkillResponse();
            var setLight = response.GadgetColor("0000FF", 10000);
            Assert.Equal(setLight, response.Response.Directives.First());
            Assert.True(Utility.CompareJson(setLight, "SetLightDirectiveBroadcast.json"));
        }

        [Fact]
        public void SetLightExtensionWorks()
        {
            var response = ResponseBuilder.Empty();
            var directive = response.GadgetColor("0000FF", new[] { "gadgetid1" });
            Assert.Single(response.Response.Directives);
            Assert.Equal(directive, response.Response.Directives.First());
            Assert.IsType<SetLightDirective>(directive);
            Assert.Equal("0000FF", directive.Parameters.Animations.First().Sequence.First().Color);
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

        [Fact]
        public void AddRollCallGeneratesExpectedObjectModel()
        {
            var firstGadget = "first";
            var secondGadget = "second";
            var timeout = 10000;
            var response = new SkillResponse();
            var directive = response.AddRollCall(timeout, firstGadget, secondGadget);
            Assert.Single(response.Response.Directives);
            Assert.Equal(directive, response.Response.Directives.First());
            Assert.NotNull(directive);
            Assert.Null(response.Response.ShouldEndSession);

            Assert.Equal(2, directive.Proxies.Count);
            Assert.Equal(firstGadget, directive.Proxies[0]);
            Assert.Equal(secondGadget, directive.Proxies[1]);

            Assert.Equal(2, directive.Events.Count);
            var timeOutEvent = directive.Events["timed out"];

            Assert.Single(timeOutEvent.Meets);
            Assert.Equal("timed out", timeOutEvent.Meets.First());
            Assert.Equal(GadgetEventReportType.History, timeOutEvent.Reports);

            var rollCallCompleteEvent = directive.Events["rollcall complete"];
            Assert.Equal("rollcall complete", rollCallCompleteEvent.Meets.First());
            Assert.Equal(GadgetEventReportType.Matches, rollCallCompleteEvent.Reports);

            Assert.Single(directive.Recognizers);
            var rollCallRecogniser = directive.Recognizers["rollcall complete"] as PatternRecognizer;

            Assert.NotNull(rollCallRecogniser);
            Assert.True(rollCallRecogniser.Fuzzy);
            Assert.Equal(PatternRecognizerAnchor.Start, rollCallRecogniser.Anchor);
            Assert.Equal(2, rollCallRecogniser.Patterns.Count);

            var firstPattern = rollCallRecogniser.Patterns.First();
            Assert.Single(firstPattern.GadgetIds);
            Assert.Equal(firstGadget, firstPattern.GadgetIds.First());
            Assert.Equal(PatternAction.Down, firstPattern.Action);

            var secondPattern = rollCallRecogniser.Patterns.Skip(1).First();
            Assert.Single(secondPattern.GadgetIds);
            Assert.Equal(secondGadget, secondPattern.GadgetIds.First());
            Assert.Equal(PatternAction.Down, secondPattern.Action);
        }

        [Fact]
        public void TryRollCallReturnFalseIfEventDoesntMatch()
        {
            var request = new SkillRequest();
            request.Request = new InputHandlerEventRequest
            {
                Events = new[] { new GadgetEvent { Name = "timed out" } }
            };
            Assert.False(((InputHandlerEventRequest)request.Request).TryRollCallResult(out Dictionary<string, string> results));
        }

        [Fact]
        public void TryRollCallReturnTrueIfEventMatches()
        {
            var request = new SkillRequest();
            request.Request = new InputHandlerEventRequest
            {
                Events = new[] { new GadgetEvent {
                    Name = GameEngineExtensions.RollCallCompleteName ,
                                        InputEvents = new[]{
                        new InputEvent{GadgetId="first"},
                        new InputEvent{GadgetId="second"}
                        }}
                        }
            };
            Assert.True(((InputHandlerEventRequest)request.Request).TryRollCallResult(out Dictionary<string, string> results, "first", "second"));
        }

        [Fact]
        public void TryRollCallThrowsExceptionOnInputMismatch()
        {
            var request = new SkillRequest();
            request.Request = new InputHandlerEventRequest
            {
                Events = new[] { new GadgetEvent {
                    Name = GameEngineExtensions.RollCallCompleteName,
                    InputEvents = new[]{
                        new InputEvent{GadgetId="first"},
                        new InputEvent{GadgetId="second"}
                        }}}
            };

            Assert.Throws<InvalidOperationException>(() => ((InputHandlerEventRequest)request.Request).TryRollCallResult(out Dictionary<string, string> results, "first"));

        }

        [Fact]
        public void TryRollCallGeneratesDictionaryWithValidData()
        {
            var request = new SkillRequest();
            request.Request = new InputHandlerEventRequest
            {
                Events = new[] { new GadgetEvent {
                    Name = GameEngineExtensions.RollCallCompleteName,
                    InputEvents = new[]{
                        new InputEvent{GadgetId="xxx"},
                        new InputEvent{GadgetId="yyy"}
                        }}}
            };

            ((InputHandlerEventRequest)request.Request).TryRollCallResult(out Dictionary<string, string> results, "first", "second");
            Assert.Equal(2, results.Count);
            Assert.Equal("xxx", results["first"]);
            Assert.Equal("yyy", results["second"]);

        }

        [Fact]
        public void TryRollCallGeneratesPartialDictionaryWithOptional()
        {
            var request = Utility.ExampleFileContent<SkillRequest>("TimedOutGadgets.json");
            var result = ((InputHandlerEventRequest)request.Request).TryRollCallOptionalResult(out Dictionary<string, string> mapping, "first", "second", "third", "fourth");
            Assert.True(result);
            Assert.Equal(2, mapping.Count);
            Assert.Equal("amzn1.ask.gadget.05RPH7PJ", mapping["first"]);
            Assert.Equal("amzn1.ask.gadget.05RP0000", mapping["second"]);
        }

        [Fact]
        public void TriggerWhenButtonDownGadgetIdArrayGeneratesCorrectly()
        {
            var gadget1 = "xxx";
            var gadget2 = "yyy";
            var response = new SkillResponse();
            var result = response.WhenFirstButtonDown(new[] { gadget1, gadget2 }, "eventName", 10000);
            Assert.NotNull(result);

            AssertCreatedButtonDownTriggerFor(response, gadget1, gadget2);
        }

        [Fact]
        public void TriggerWhenButtonDownMappingDictionaryGeneratesCorrectly()
        {
            var mapping = new Dictionary<string, string>
            {
                {"gadget1", "xxx"},
                {"gadget2", "yyy"},
            };

            var response = new SkillResponse();
            var result = response.WhenFirstButtonDown(mapping, "eventName", 10000);
            Assert.NotNull(result);

            AssertCreatedButtonDownTriggerFor(response, mapping.Values.ToArray());
        }

        private void AssertCreatedButtonDownTriggerFor(SkillResponse response, params string[] gadgetIds)
        {
            if (gadgetIds.Length == 0)
                throw new ArgumentException("Value cannot be an empty collection.", nameof(gadgetIds));

            Assert.Single(response.Response.Directives);
            Assert.IsType<StartInputHandlerDirective>(response.Response.Directives.First());

            var directive = (StartInputHandlerDirective)response.Response.Directives.First();
            Assert.Equal(10000, directive.TimeoutMilliseconds);
            Assert.Equal(2, directive.Events.Count);
            Assert.Equal("timed out", directive.Events.First().Key);
            Assert.Equal("eventName", directive.Events.Skip(1).First().Key);

            var namedEvent = directive.Events["eventName"];
            Assert.Single(namedEvent.Meets);
            Assert.Equal("eventName", namedEvent.Meets.First());

            Assert.Single(directive.Recognizers);
            Assert.Equal("eventName", directive.Recognizers.First().Key);
            Assert.IsType<PatternRecognizer>(directive.Recognizers.First().Value);

            var recogniser = (PatternRecognizer)directive.Recognizers["eventName"];
            Assert.Equal(gadgetIds.Length,recogniser.GadgetIds.Count);
            Assert.True(recogniser.GadgetIds.All(g => gadgetIds.Contains(g)));

            Assert.True(recogniser.Fuzzy);
            Assert.Single(recogniser.Patterns);
            Assert.Equal(ButtonAction.Down, recogniser.Patterns.First().Action);
            Assert.Null(recogniser.Patterns.First().Repeat);
        }

        [Fact]
        public void SuccessfulTriggerEventFindsSingleGadget()
        {
            RequestConverterHelper.AddGadgetRequests();
            var request = Utility.ExampleFileContent<SkillRequest>("TimedOutGadgets.json");
            var result = ((InputHandlerEventRequest)request.Request).TryMapEventGadget("timed out", out var gadgetId);
            Assert.True(result);
            Assert.Equal("amzn1.ask.gadget.05RPH7PJ",gadgetId);
        }

        [Fact]
        public void SuccessfulTriggerEventFindsMultipleGadgets()
        {
            var request = new SkillRequest
            {
                Request = new InputHandlerEventRequest
                {
                    Events = new[]
                    {
                        new GadgetEvent
                        {
                            Name = GameEngineExtensions.RollCallCompleteName,
                            InputEvents = new[]
                            {
                                new InputEvent {GadgetId = "xxx"},
                                new InputEvent {GadgetId = "yyy"}
                            }
                        }
                    }
                }
            };

            var result = ((InputHandlerEventRequest)request.Request).TryMapEventGadgets(GameEngineExtensions.RollCallCompleteName, out var results, "first", "second");
            Assert.True(result);
            Assert.Equal(2, results.Count);
            Assert.Equal("xxx", results["first"]);
            Assert.Equal("yyy", results["second"]);
            
        }

        [Fact]
        public void IncorrectTriggerReturnsCorrectResponse()
        {
            var request = Utility.ExampleFileContent<SkillRequest>("TimedOutGadgets.json");
            var result = ((InputHandlerEventRequest) request.Request).TryMapEventGadget("eventName", out var gadgetId);
            Assert.False(result);
            Assert.Null(gadgetId);
        }
    }
}
