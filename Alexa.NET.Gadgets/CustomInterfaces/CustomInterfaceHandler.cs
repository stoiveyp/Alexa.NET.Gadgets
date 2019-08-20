using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alexa.NET.Request.Type;

namespace Alexa.NET.Gadgets.CustomInterfaces
{
    public class CustomInterfaceHandler: IRequestTypeConverter
    {
        public bool CanConvert(string requestType)
        {
            return requestType?.StartsWith("CustomInterfaceController") ?? false;
        }

        public Request.Type.Request Convert(string requestType)
        {
            switch (requestType)
            {
                case "CustomInterfaceController.EventsReceived":
                    return new EventsReceivedRequest();
                case "CustomInterfaceController.Expired":
                    return new ExpiredRequest();
            }

            throw new InvalidOperationException("Unable to convert " + requestType);
        }

        public void AddToRequestConverter()
        {
            if (RequestConverter.RequestConverters.Where(rc => rc != null)
                .All(rc => rc.GetType() != typeof(CustomInterfaceHandler)))
            {
                RequestConverter.RequestConverters.Add(this);
            }
        }
    }
}
