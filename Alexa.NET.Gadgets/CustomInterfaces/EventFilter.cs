using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Alexa.NET.Gadgets.CustomInterfaces
{
    public class EventFilter
    {
        public EventFilter() { }

        public EventFilter(FilterExpression expression, FilterMatchAction action)
        {
            FilterExpression = expression;
            FilterMatchAction = action;
        }

        [JsonProperty("filterExpression")]
        public FilterExpression FilterExpression { get; set; }

        [JsonProperty("filterMatchAction"),JsonConverter(typeof(StringEnumConverter))]
        public FilterMatchAction FilterMatchAction { get; set; }
    }
}