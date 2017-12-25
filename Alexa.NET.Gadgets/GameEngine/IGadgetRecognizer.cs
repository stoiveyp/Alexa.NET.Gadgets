using Newtonsoft.Json;

namespace Alexa.NET.Response.Directive
{
    public interface IGadgetRecognizer
    {
        [JsonProperty("type")]
        string Type { get; }
    }
}