using System.Collections.Generic;
using Newtonsoft.Json;

namespace Alexa.NET.Gadgets.GameEngine
{
    public class Pattern
    {
        [JsonProperty("gadgetIds")]
        public IList<string> GadgetIds { get; set; } = new List<string>();

        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("colors")]
        public IList<string> Colors { get; set; } = new List<string>();
    }
}