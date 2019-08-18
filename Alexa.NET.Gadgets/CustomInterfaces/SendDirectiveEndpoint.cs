using Newtonsoft.Json;

namespace Alexa.NET.Gadgets.CustomInterfaces
{
    public class SendDirectiveEndpoint
    {
        public SendDirectiveEndpoint() { }

        public SendDirectiveEndpoint(string endpointId)
        {
            EndpointId = endpointId;
        }

        [JsonProperty("endpointId")]
        public string EndpointId { get; set; }
    }
}