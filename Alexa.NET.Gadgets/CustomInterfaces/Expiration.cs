using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Alexa.NET.Gadgets.CustomInterfaces
{
    public class Expiration
    {
        public Expiration() { }

        public Expiration(int milliseconds, object payload)
        {
            Milliseconds = milliseconds;
            Payload = payload;
        }

        [JsonProperty("durationInMilliseconds")]
        public int Milliseconds { get; set; }

        [JsonProperty("expirationPayload")]
        public object Payload { get; set; }
    }
}