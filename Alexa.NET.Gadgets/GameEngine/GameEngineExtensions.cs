using System;
using System.Collections.Generic;
using Alexa.NET.Response;
using System.Linq;
using Alexa.NET.Gadgets.GameEngine.Directives;
using Alexa.NET.Gadgets.GameEngine.Requests;

namespace Alexa.NET.Gadgets.GameEngine
{
    public static class GameEngineExtensions
    {
        public const string RollCallCompleteName = "rollcall complete";
        private const string TimedOutName = "timed out";

        public static bool TryRollCallOptionalResult(this InputHandlerEventRequest request, out Dictionary<string, string> results, params string[] gadgetNames)
        {
            var rollcallEvent = request.Events.FirstOrDefault();

            if (rollcallEvent == null)
            {
                results = null;
                return false;
            }

            if (rollcallEvent.Name == RollCallCompleteName)
            {
                return TryRollCallResult(request, out results, gadgetNames);
            }

            if (rollcallEvent.Name != TimedOutName)
            {
                results = null;
                return false;
            }

            var gadgetIds = rollcallEvent.InputEvents.Where(e => e.Action == PatternAction.Down).Select(e => e.GadgetId).Distinct().ToArray();
            var maxZip = Math.Min(gadgetIds.Length, gadgetNames.Length);
            results = gadgetNames.Take(maxZip).Zip(gadgetIds.Take(maxZip),Tuple.Create).ToDictionary(a => a.Item1, a => a.Item2);
            return true;
        }

        public static bool TryRollCallResult(this InputHandlerEventRequest request, out Dictionary<string, string> results, params string[] gadgetNames)
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

            results = gadgetNames.Zip(rollcallEvent.InputEvents, (name, inputEvent) => new {name, id = inputEvent.GadgetId }).ToDictionary(a => a.name, a => a.id);
            return true;
        }

        public static StartInputHandlerDirective AddRollCall(this SkillResponse response, int timeoutMilliseconds, params string[] friendlyNames)
        {
            var directive = new StartInputHandlerDirective
            {
                TimeoutMilliseconds = timeoutMilliseconds,
                Proxies = friendlyNames.ToList()
            };


            AddTimeOutAndEvent(directive, RollCallCompleteName);
            AddRollCallRecognisers(directive, friendlyNames);
            SetDirective(response, directive);
            return directive;
        }

        public static StartInputHandlerDirective WhenFirstButtonDown(this SkillResponse response, Dictionary<string, string> gadgetNameIdMapping, string triggerEventName, int timeoutMilliseconds)
        {
            return WhenFirstButtonDown(response, gadgetNameIdMapping.Values.ToArray(), triggerEventName,timeoutMilliseconds);
        }

        public static StartInputHandlerDirective WhenFirstButtonDown(this SkillResponse response, string[] possibleGadgetIds, string triggerEventName, int timeoutMilliseconds)
        {
            var directive = new StartInputHandlerDirective {TimeoutMilliseconds = timeoutMilliseconds};
            AddTimeOutAndEvent(directive, triggerEventName);
            AddButtonDownTrigger(directive, triggerEventName, possibleGadgetIds);

            SetDirective(response,directive);
            return directive;
        }

        private static void AddButtonDownTrigger(StartInputHandlerDirective directive, string triggerEventName, string[] possibleGadgetIds)
        {
            var recogniser = new PatternRecognizer
            {
                GadgetIds = possibleGadgetIds.ToList(),
                Fuzzy = true
            };

            recogniser.Patterns.Add(new Pattern{Action = ButtonAction.Down});

            directive.Recognizers.Add(triggerEventName,recogniser);
        }

        public static bool TryMapEventGadget(this InputHandlerEventRequest request, string eventName, out string gadgetId)
        {
            var gadgetEvent = request.Events.FirstOrDefault(e => e.Name == eventName);
            if (gadgetEvent == null)
            {
                gadgetId = null;
                return false;
            }

            gadgetId = GadgetIds(gadgetEvent).FirstOrDefault();
            return gadgetId != null;
        }

        public static bool TryMapEventGadgets(this InputHandlerEventRequest request, string eventName,
            out Dictionary<string, string> results, params string[] gadgetNames)
        {
            var gadgetEvent = request.Events.FirstOrDefault(e => e.Name == eventName);
            if (gadgetEvent == null)
            {
                results = null;
                return false;
            }

            var maxZip = Math.Min(GadgetIds(gadgetEvent).Count(), gadgetNames.Length);
            results = gadgetNames.Take(maxZip).Zip(GadgetIds(gadgetEvent).Take(maxZip), Tuple.Create).ToDictionary(a => a.Item1, a => a.Item2);
            return results.Any();
        }

        private static IEnumerable<string> GadgetIds(GadgetEvent request)
        {
            return request.InputEvents.Select(e => e.GadgetId).Distinct();
        }

        private static void AddRollCallRecognisers(StartInputHandlerDirective directive, string[] names)
        {
            var recogniser = new PatternRecognizer
            {
                Fuzzy = true,
                Anchor = PatternRecognizerAnchor.Start
            };
            directive.Recognizers.Add(RollCallCompleteName, recogniser);

            foreach (var name in names)
            {
                var pattern = CreateRollCallPattern(name);
                recogniser.Patterns.Add(pattern);
            }
        }

        private static Pattern CreateRollCallPattern(string name)
        {
            return new Pattern
            {
                Action = PatternAction.Down,
                GadgetIds = new List<string> { name }
            };
        }

        private static void AddTimeOutAndEvent(StartInputHandlerDirective directive, string eventName, string meetTrigger = null)
        {
            directive.Events.Add("timed out", new InputHandlerEvent
            {
                Meets = new List<string> { "timed out" },
                EndInputHandler = true,
                Reports = GadgetEventReportType.History
            });

            directive.Events.Add(eventName, new InputHandlerEvent
            {
                Meets = new List<string> { meetTrigger ?? eventName },
                EndInputHandler = true,
                Reports = GadgetEventReportType.Matches
            });

        }

        private static void SetDirective(SkillResponse response, IDirective directive)
        {
            if (response == null)
            {
                throw new InvalidOperationException("Unable to set directive on null response");
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
