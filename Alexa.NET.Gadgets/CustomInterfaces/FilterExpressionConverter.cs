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
            reader.Read();
            var op = reader.Value.ToString();
            reader.Read();
            switch (op)
            {
                case "and":
                case "or":
                    var combined = new CombinedFilterExpression { Operator = (CombinationOperator)Enum.Parse(typeof(CombinationOperator), char.ToUpper(op[0]) + op.Substring(1)) };
                    return ProcessCombinedExpression(combined, reader, serializer);
                default:
                    var comparison = new ComparisonFilterExpression { Operator = (ComparisonOperator)Enum.Parse(typeof(ComparisonOperator), op) };
                    return ProcessComparisonFilter(comparison, reader, serializer);
            }
        }

        private ComparisonFilterExpression ProcessComparisonFilter(ComparisonFilterExpression expression, JsonReader reader, JsonSerializer serializer)
        {
            reader.Skip();
            return expression;
        }

        private CombinedFilterExpression ProcessCombinedExpression(CombinedFilterExpression expression, JsonReader reader, JsonSerializer serializer)
        {
            reader.Skip();
            return expression;
        }

        private static readonly Type ExpressionType = typeof(FilterExpression);

        public override bool CanConvert(Type objectType)
        {
            return ExpressionType.IsAssignableFrom(objectType);
        }
    }
}