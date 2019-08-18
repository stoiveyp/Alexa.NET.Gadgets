using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Alexa.NET.Gadgets.CustomInterfaces
{
    public class Endpoint
    {
        [JsonProperty("endpointId")]
        public string EndpointId { get; set; }

        [JsonProperty("friendlyName")]
        public string FriendlyName { get; set; }

        [JsonProperty("capabilities")]
        public Capability[] Capabilities { get; set; }
    }
}
