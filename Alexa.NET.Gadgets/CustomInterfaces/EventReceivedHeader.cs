using Newtonsoft.Json;

namespace Alexa.NET.Gadgets.CustomInterfaces
{
    public class EventReceivedHeader
    {
        [JsonProperty("namespace")]
        public string Namespace { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}