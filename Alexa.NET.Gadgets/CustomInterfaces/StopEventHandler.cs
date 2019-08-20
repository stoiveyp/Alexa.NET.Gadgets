using System;
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

        public StopEventHandler() { }

        public StopEventHandler(Guid token)
        {
            Token = token;
        }

        [JsonProperty("type")]
        public string Type => DirectiveType;

        [JsonProperty("token")]
        public Guid Token { get; set; }
    }
}