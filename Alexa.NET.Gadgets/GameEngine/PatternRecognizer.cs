using System.Collections.Generic;
using Newtonsoft.Json;

namespace Alexa.NET.Gadgets.GameEngine
{
    public class PatternRecognizer:IGadgetRecognizer
    {
        public string Type => "match";

        [JsonProperty("anchor")]
        public string Anchor { get; set; }

        [JsonProperty("fuzzy")]
        public bool Fuzzy { get; set; }

        [JsonProperty("gadgetIds")]
        public IList<string> GadgetIds { get; set; } = new List<string>();

        [JsonProperty("actions")]
        public IList<string> Actions { get; set; } = new List<string>();

        [JsonProperty("pattern")]
        public IList<Pattern> Patterns { get; set; } = new List<Pattern>();
    }
}
