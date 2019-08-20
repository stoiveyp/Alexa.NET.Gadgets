using Newtonsoft.Json;

namespace Alexa.NET.Gadgets.CustomInterfaces
{
    public class Capability
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("interface")]
        public string Interface { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }
    }
}