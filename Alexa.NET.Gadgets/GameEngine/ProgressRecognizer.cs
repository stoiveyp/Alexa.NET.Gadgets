using Newtonsoft.Json;

namespace Alexa.NET.Response.Directive
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
