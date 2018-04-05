using System;
using System.Collections.Generic;
using Alexa.NET.Response;
using System.Linq;
using Alexa.NET.Gadgets.GameEngine.Directives;

namespace Alexa.NET.Gadgets.GameEngine
{
    public static class GameEngineExtensions
    {
        public const string RollCallCompleteName = "rollcall complete";

        public static bool TryRollCallResult(this GameEngine.Requests.InputHandlerEventRequest request, out Dictionary<string, string> results, params string[] gadgetNames)
        {
            results = null;
            var rollcallEvent = request.Events.FirstOrDefault(ge => ge.Name == RollCallCompleteName);
            if (rollcallEvent == null)
            {
                return false;
            }

            if (gadgetNames.Length != rollcallEvent.InputEvents.Length)
            {
                throw new InvalidOperationException($"Gadget Mismatch - roll call event has returned {rollcallEvent.InputEvents.Length} events and there are {gadgetNames.Length} names");
            }

            results = gadgetNames.Zip(rollcallEvent.InputEvents, (name, inputEvent) => new { name = name, id = inputEvent.GadgetId }).ToDictionary(a => a.name, a => a.id);
            return true;
        }

        public static StartInputHandlerDirective AddRollCall(this SkillResponse response, int timeoutMilliseconds, params string[] friendlyNames)
        {
            var directive = new StartInputHandlerDirective
            {
                TimeoutMilliseconds = timeoutMilliseconds,
                Proxies = friendlyNames.ToList()
            };


            AddEvents(directive);
            AddRecognisers(directive, friendlyNames);
            SetDirective(response, directive);
            return directive;
        }

        private static void AddRecognisers(StartInputHandlerDirective directive, string[] names)
        {
            var recogniser = new PatternRecognizer
            {
                Fuzzy = true,
                Anchor = PatternRecognizerAnchor.Start
            };
            directive.Recognizers.Add(RollCallCompleteName, recogniser);

            foreach (var name in names)
            {
                var pattern = CreatePattern(name);
                recogniser.Patterns.Add(pattern);
            }
        }

        private static Pattern CreatePattern(string name)
        {
            return new Pattern
            {
                Repeat = 1,
                Action = PatternAction.Down,
                GadgetIds = new List<string> { name }
            };
        }

        private static void AddEvents(StartInputHandlerDirective directive)
        {
            directive.Events.Add("timed out", new InputHandlerEvent
            {
                Meets = new List<string> { "timed out" },
                EndInputHandler = true,
                Reports = GadgetEventReportType.History
            });

            directive.Events.Add(RollCallCompleteName, new InputHandlerEvent
            {
                Meets = new List<string> { RollCallCompleteName },
                EndInputHandler = true,
                Reports = GadgetEventReportType.Matches
            });

        }

        private static void SetDirective(SkillResponse response, IDirective directive)
        {
            if (response == null)
            {
                throw new InvalidOperationException("Unable to set gadget colors on null response");
            }

            if (response.Response == null)
            {
                response.Response = new ResponseBody();
            }

            if (response.Response.Directives == null)
            {
                response.Response.Directives = new List<IDirective>();
            }
            response.Response.Directives.Add(directive);
        }
    }
}