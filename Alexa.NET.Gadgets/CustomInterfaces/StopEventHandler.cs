using Alexa.NET.Response;
using Alexa.NET.Response.Converters;
using Newtonsoft.Json;

namespace Alexa.NET.Gadgets.CustomInterfaces
{
    public class StopEventHandler : IDirective
    {
        public const string DirectiveType = "CustomInterfaceController.StopEventHandler";

        public static void AddToDirectiveConverter()
        {
            if (!DirectiveConverter.TypeFactories.ContainsKey(DirectiveType))
            {
                DirectiveConverter.TypeFactories.Add(DirectiveType, () => new StopEventHandler());
            }
        }

        public string Type => DirectiveType;

        [JsonProperty("token")]
        public string Token { get; set; }
    }
}