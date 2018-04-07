using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace Alexa.NET.Gadgets.GameEngine.Directives
{
    public class InputHandlerEvent
    {
        [JsonProperty("meets")]
        public IList<string> Meets { get; set; } = new List<string>();

        [JsonProperty("fails")]
        public IList<string> Fails { get; set; } = new List<string>();

        [JsonProperty("reports")]
        public string Reports { get; set; }

        [JsonProperty("shouldEndInputHandler")]
        public bool EndInputHandler { get; set; }

        [JsonProperty("maximumInvocations",NullValueHandling = NullValueHandling.Ignore)]
        public int? MaximumInvocations { get; set; }

        [JsonProperty("triggerTimeMilliseconds",NullValueHandling = NullValueHandling.Ignore)]
        public int? TriggerTimeMilliseconds { get; set; }

        public bool ShouldSerializeMeets()
        {
            return Meets?.Any() ?? false;
        }

        public bool ShouldSerializeFails()
        {
            return Fails?.Any() ?? false;
        }
    }
}