using System;
using Newtonsoft.Json;

namespace Alexa.NET.Gadgets.CustomInterfaces
{
    public class ExpiredRequest : Request.Type.Request
    {
        [JsonProperty("token")]
        public Guid Token { get; set; }

        [JsonProperty("expirationPayload")]
        public object ExpirationPayload { get; set; }
    }
}