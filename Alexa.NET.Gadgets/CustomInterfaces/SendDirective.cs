using Alexa.NET.Response;
using Alexa.NET.Response.Converters;
using Newtonsoft.Json;

namespace Alexa.NET.Gadgets.CustomInterfaces
{
    public class SendDirective : IDirective
    {
        public SendDirective() { }

        public SendDirective(string endpointId, string nsName, string name, object payload)
        {
            Endpoint = new SendDirectiveEndpoint(endpointId);
            Header = new Header(nsName, name);
            Payload = payload;
        }

        public static void AddToDirectiveConverter()
        {
            if(!DirectiveConverter.TypeFactories.ContainsKey(DirectiveType))
            {
                DirectiveConverter.TypeFactories.Add(DirectiveType,() => new SendDirective());
            }
        }

        public const string DirectiveType = "CustomInterfaceController.SendDirective";

        [JsonProperty("type")]
        public string Type => DirectiveType;

        [JsonProperty("endpoint")]
        public SendDirectiveEndpoint Endpoint { get; set; }

        [JsonProperty("header")]
        public Header Header { get; set; }

        [JsonProperty("payload")]
        public object Payload { get; set; }
    }
}
