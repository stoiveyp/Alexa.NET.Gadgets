using Newtonsoft.Json;

namespace Alexa.NET.Gadgets.GameEngine
{
    public class ProgressRecognizer:IGadgetRecognizer
    {
        [JsonProperty("type")]
        public string Type => "progress";

        [JsonProperty("recognizer")]
        public string Recognizer { get; set; }

        [JsonProperty("completion")]
        public int Completion { get; set; }
    }
}
