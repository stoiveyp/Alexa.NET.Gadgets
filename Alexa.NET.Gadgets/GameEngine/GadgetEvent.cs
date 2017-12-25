using System.Collections.Generic;
using Newtonsoft.Json;

namespace Alexa.NET.Gadgets.GameEngine
{
    public class GadgetEvent
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
    }
}