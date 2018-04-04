using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Alexa.NET.Gadgets.GadgetController
{
    public class SetLightAnimation
    {
        [JsonProperty("repeat")] public int Repeat { get; set; } = 1;

        [JsonProperty("targetLights")]
        public List<string> TargetLights { get; set; } = new List<string>();
        
        [JsonProperty("sequence")]
        public List<AnimationSegment> Sequence { get; set; } = new List<AnimationSegment>();

        public static SetLightAnimation Create(int repeat, IEnumerable<string> targets, params AnimationSegment[] sequence)
        {
            return new SetLightAnimation
            {
                Repeat = repeat,
                TargetLights = targets.ToList(),
                Sequence = sequence.ToList()
            };
        }
    }
}