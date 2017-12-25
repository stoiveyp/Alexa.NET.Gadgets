using Newtonsoft.Json;

namespace Alexa.NET.Gadgets.GameEngine.Directives
{
    public interface IGadgetRecognizer
    {
        [JsonProperty("type")]
        string Type { get; }
    }
}