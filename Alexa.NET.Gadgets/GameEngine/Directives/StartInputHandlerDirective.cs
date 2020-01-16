using System.Collections.Generic;
using Alexa.NET.Response;
using Newtonsoft.Json;

namespace Alexa.NET.Gadgets.GameEngine.Directives
{
    public class StartInputHandlerDirective:IEndSessionDirective
    {
        [JsonProperty("type")]
        public string Type => "GameEngine.StartInputHandler";

        [JsonProperty("timeout")]
        public int TimeoutMilliseconds { get; set; }

        [JsonProperty("maximumHistoryLength")]
        public int MaximumHistoryLength { get; set; }

        [JsonProperty("proxies")]
        public IList<string> Proxies { get; set; } = new List<string>();

        [JsonProperty("recognizers")]
        public IDictionary<string, IGadgetRecognizer> Recognizers { get; set; } = new Dictionary<string, IGadgetRecognizer>();

        [JsonProperty("events")]
        public IDictionary<string,InputHandlerEvent> Events { get; set; } = new Dictionary<string, InputHandlerEvent>();

        public bool? ShouldEndSession => null;
    }
}
