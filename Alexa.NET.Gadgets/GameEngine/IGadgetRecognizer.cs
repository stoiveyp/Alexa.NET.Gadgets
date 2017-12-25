using Newtonsoft.Json;

namespace Alexa.NET.Gadgets.GameEngine
{
    public interface IGadgetRecognizer
    {
        [JsonProperty("type")]
        string Type { get; }
    }
}