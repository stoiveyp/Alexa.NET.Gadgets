using Alexa.NET.Response;
using Newtonsoft.Json;

namespace Alexa.NET.Gadgets.GameEngine.Directives
{
    public class StopInputHandlerDirective:IDirective
    {
        [JsonProperty("type")] public string Type => "GameEngine.StopInputHandler";
        [JsonProperty("originatingRequestId")]public string OriginatingRequestId { get; set; }
    }
}
