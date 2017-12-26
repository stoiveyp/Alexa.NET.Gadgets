using System.Linq;
using Alexa.NET.Request.Type;

namespace Alexa.NET.Gadgets.GameEngine.Requests
{
    public class GadgetRequestHandler: IRequestTypeConverter
    {
        private const string InputHandlerType = "GameEngine.InputHandlerEvent";

        public bool CanConvert(string requestType)
        {
            return requestType == InputHandlerType;
        }

        public Request.Type.Request Convert(string requestType)
        {
            if (requestType == InputHandlerType)
            {
                return new InputHandlerEventRequest();
            }

            return null;
        }

        public void AddToRequestConverter()
        {
            if (RequestConverter.RequestConverters.Where(rc => rc != null)
                .All(rc => rc.GetType() != typeof(GadgetRequestHandler)))
            {
                RequestConverter.RequestConverters.Add(this);
            }
        }
    }
}
