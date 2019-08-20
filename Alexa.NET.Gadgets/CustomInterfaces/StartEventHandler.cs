using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Alexa.NET.Response;
using Alexa.NET.Response.Converters;
using Newtonsoft.Json;

namespace Alexa.NET.Gadgets.CustomInterfaces
{
    public class StartEventHandler:IDirective
    {
        public const string DirectiveType = "CustomInterfaceController.StartEventHandler";

        public static void AddToDirectiveConverter()
        {
            if (!DirectiveConverter.TypeFactories.ContainsKey(DirectiveType))
            {
                DirectiveConverter.TypeFactories.Add(DirectiveType, () => new StartEventHandler());
            }
        }

        public StartEventHandler() { }

        public StartEventHandler(Guid token, Expiration expiration, FilterMatchAction action, FilterExpression expression)
        {
            Token = token;
            Expiration = expiration;
            EventFilter = new EventFilter(expression, action);
        }

        [JsonProperty("type")]
        public string Type => DirectiveType;

        [JsonProperty("token")]
        public Guid Token { get; set; }

        [JsonProperty("expiration")]
        public Expiration Expiration { get; set; }

        [JsonProperty("eventFilter")]
        public EventFilter EventFilter { get; set; } 
    }
}
