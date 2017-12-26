using Alexa.NET.Gadgets.GameEngine.Directives;
using Newtonsoft.Json;

namespace Alexa.NET.Gadgets.GameEngine.Requests
{
    public class InputHandlerEventRequest: Request.Type.Request
    {
        [JsonProperty("originatingRequestId")]
        public string OriginatingRequestId { get; set; }

        [JsonProperty("events")]
        public GadgetEvent[] Events { get; set; }
    }
}