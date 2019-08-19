using System;
using Newtonsoft.Json;

namespace Alexa.NET.Gadgets.CustomInterfaces
{
    public class EventsReceivedRequest : Request.Type.Request
    {
        [JsonProperty("token")]
        public Guid Token { get; set; }

        [JsonProperty("events")]
        public EventReceived[] Events { get; set; }
    }
}
