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

        public static AnimationSegment Create(string color, int duration, bool blend = false)
        {
            return new AnimationSegment { Color = color, DurationMilliseconds = duration, Blend = blend };
        }
    }
}