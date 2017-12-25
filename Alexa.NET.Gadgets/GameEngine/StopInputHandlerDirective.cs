using Alexa.NET.Response;
using Newtonsoft.Json;

namespace Alexa.NET.Gadgets.GameEngine
{
    public class StopInputHandlerDirective:IDirective
    {
        [JsonProperty("type")] public string Type => "GameEngine.StopInputHandler";
        [JsonProperty("originatingRequestId")]public string OriginatingRequestId { get; set; }
    }
}
