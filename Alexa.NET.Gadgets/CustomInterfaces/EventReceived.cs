using Newtonsoft.Json;

namespace Alexa.NET.Gadgets.CustomInterfaces
{
    public class EventReceived
    {
        [JsonProperty("header")]
        public EventReceivedHeader Header { get; set; }

        [JsonProperty("endpoint")]
        public Endpoint Endpoint { get; set; }

        [JsonProperty("payload")]
        public object Payload { get; set; }
    }
}