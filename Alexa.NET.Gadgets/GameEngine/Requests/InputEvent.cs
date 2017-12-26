using System;
using Alexa.NET.Helpers;
using Newtonsoft.Json;

namespace Alexa.NET.Gadgets.GameEngine.Requests
{
    public class InputEvent
    {
        [JsonProperty("gadgetId")]
        public string GadgetId { get; set; }

        [JsonProperty("timestamp")]
        [JsonConverter(typeof(MixedDateTimeConverter))]
        public DateTime Timestamp { get; set; }

        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("feature")]
        public string Feature { get; set; }
    }
}