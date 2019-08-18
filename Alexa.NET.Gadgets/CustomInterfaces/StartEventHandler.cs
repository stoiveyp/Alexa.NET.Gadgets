using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.Response;
using Alexa.NET.Response.Converters;

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

        public string Type => DirectiveType;
    }
}
