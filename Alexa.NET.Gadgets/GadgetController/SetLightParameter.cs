using System.Collections.Generic;
using Newtonsoft.Json;

namespace Alexa.NET.Gadgets.GadgetController
{
    public class SetLightParameter
    {
        [JsonProperty("triggerEvent")]
        public string TriggerEvent { get; set; }

        [JsonProperty("triggerEventTimeMs")]
        public int TriggerEventTimeMilliseconds { get; set; }

        [JsonProperty("animations")]
        public SetLightAnimation Animations { get; set; }
    }
}