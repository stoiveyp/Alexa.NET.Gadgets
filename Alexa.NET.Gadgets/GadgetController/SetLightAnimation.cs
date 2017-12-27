using System.Collections.Generic;
using Newtonsoft.Json;

namespace Alexa.NET.Gadgets.GadgetController
{
    public class SetLightAnimation
    {
        [JsonProperty("repeat")]
        public int Repeat { get; set; }

        [JsonProperty("targetLights")]
        public List<int> TargetLights { get; set; } = new List<int>();

        [JsonProperty("sequence")]
        public List<AnimationSegment> Sequence { get; set; } = new List<AnimationSegment>();

    }
}