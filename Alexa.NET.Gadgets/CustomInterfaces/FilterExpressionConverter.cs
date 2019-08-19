using System;
using Newtonsoft.Json;

namespace Alexa.NET.Gadgets.CustomInterfaces
{
    internal class FilterExpressionConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var filter = (FilterExpression)value;

            writer.WriteStartObject();
            writer.WritePropertyName(filter.GetOperator());

            writer.WriteStartArray();
            foreach (var item in filter.ArrayItems())
            {
                serializer.Serialize(writer,item);
            }

            writer.WriteEndArray();
            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var op = reader.ReadAsString();
            reader.Read(); //Start of Array set


            switch (op)
            {
                case "and":
                case "or":
                    var combined = new CombinedFilterExpression { Operator = (CombinationOperator)Enum.Parse(typeof(CombinationOperator), op) };
                    return ProcessCombinedExpression(combined, reader, serializer);
                default:
                    var comparison = new ComparisonFilterExpression { Operator = (ComparisonOperator)Enum.Parse(typeof(ComparisonOperator), op) };
                    return ProcessComparisonFilter(comparison, reader, serializer);
            }
        }

        private ComparisonFilterExpression ProcessComparisonFilter(ComparisonFilterExpression expression, JsonReader reader, JsonSerializer serializer)
        {
            return expression;
        }

        private CombinedFilterExpression ProcessCombinedExpression(CombinedFilterExpression expression, JsonReader reader, JsonSerializer serializer)
        {
            return expression;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(FilterExpression);
        }
    }
}