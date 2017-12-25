using Newtonsoft.Json;

namespace Alexa.NET.Gadgets.GameEngine
{
    public class DeviationRecognizer : IGadgetRecognizer
    {
        [JsonProperty("type")]
        public string Type => "deviation";

        [JsonProperty("recognizer")]
        public string Recognizer { get; set; }
    }
}
