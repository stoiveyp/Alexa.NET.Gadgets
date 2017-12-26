using Newtonsoft.Json;

namespace Alexa.NET.Gadgets.GameEngine.Requests
{
    public class GadgetEvent
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("inputEvents")]
        public InputEvent[] InputEvents { get; set; }
    }
}