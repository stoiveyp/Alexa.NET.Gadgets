using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.Request.Type;

namespace Alexa.NET.Gadgets.GameEngine.Requests
{
    public static class RequestConverterHelper
    {
        public static void AddGadgetRequests()
        {
            RequestConverter.RequestConverters.Add(new GadgetRequestHandler());
        }
    }
}
