using Newtonsoft.Json;

namespace Alexa.NET.Gadgets.GadgetController
{
    public class AnimationSegment
    {
        [JsonProperty("durationMs")]
        public int DurationMilliseconds { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("blend")]
        public bool Blend { get; set; }
    }
}