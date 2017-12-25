using System.Collections.Generic;
using Newtonsoft.Json;

namespace Alexa.NET.Response.Directive
{
    public class StartInputHandlerDirective:IDirective
    {
        [JsonProperty("type")]
        public string Type => "GameEngine.StartInputHandler";

        [JsonProperty("timeout")]
        public int TimeoutMilliseconds { get; set; }

        [JsonProperty("maximumHistoryLength")]
        public int MaximumHistoryLength { get; set; }

        [JsonProperty("proxies")]
        public IList<IGadgetProxy> Proxies { get; set; } = new List<IGadgetProxy>();

        [JsonProperty("recognizers")]
        public IDictionary<string, IGadgetRecognizer> Recognizers { get; set; } = new Dictionary<string, IGadgetRecognizer>();

        [JsonProperty("events")]
        public IDictionary<string,IGadgetEvent> Events { get; set; } = new Dictionary<string, IGadgetEvent>();
    }
}
