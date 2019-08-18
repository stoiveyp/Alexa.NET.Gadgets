using Newtonsoft.Json;

namespace Alexa.NET.Gadgets.CustomInterfaces
{
    public class Header
    {
        public Header() { }

        public Header(string nsName, string name)
        {
            Namespace = nsName;
            Name = name;
        }

        [JsonProperty("namespace")]
        public string Namespace { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}