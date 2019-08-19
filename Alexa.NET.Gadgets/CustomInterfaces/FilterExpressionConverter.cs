using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

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
            while (reader.TokenType != JsonToken.StartObject)
            {
                reader.Read();
            }

            reader.Read();

            var op = reader.Value.ToString();
           // reader.Read();
            switch (op)
            {
                case "and":
                case "or":
                    reader.Read();
                    var combined = new CombinedFilterExpression { Operator = (CombinationOperator)Enum.Parse(typeof(CombinationOperator), char.ToUpper(op[0]) + op.Substring(1)) };
                    var combineitem = ProcessCombinedExpression(combined, reader, serializer);
                    return combineitem;
                default:
                    var comparison = new ComparisonFilterExpression { Operator = GetComparison(op) };
                    var compareitem = ProcessComparisonFilter(comparison, reader, serializer);
                    return compareitem;
            }
        }

        private ComparisonFilterExpression ProcessComparisonFilter(ComparisonFilterExpression expression, JsonReader reader, JsonSerializer serializer)
        {
            reader.Read(); //Start Array

            reader.Read(); //Start Object
            reader.Read(); // Property name
            reader.Read(); // Property Value
            expression.Variable = reader.Value.ToString();
            reader.Read(); // End Object

            reader.Read(); //Value
            expression.Value = reader.Value;
            while (reader.TokenType != JsonToken.EndObject)
            {
                reader.Read(); //End Array
            }

            return expression;
        }

        private CombinedFilterExpression ProcessCombinedExpression(CombinedFilterExpression expression, JsonReader reader, JsonSerializer serializer)
        {
            reader.Read(); // Start Array
            var list = new List<FilterExpression>();
            while (reader.TokenType != JsonToken.EndArray)
            {
                var filter = serializer.Deserialize<FilterExpression>(reader);
                list.Add(filter);
                if (reader.TokenType == JsonToken.EndObject)
                {
                    reader.Read();
                }
            }

            expression.Filters = list.ToArray();
            while (reader.TokenType != JsonToken.EndObject)
            {
                reader.Read(); //End Array
            }
            return expression;
        }

        private static readonly Type ExpressionType = typeof(FilterExpression);
        private static readonly FieldInfo[] ComparisonMembers = typeof(ComparisonOperator).GetFields(BindingFlags.Public | BindingFlags.Static);

        private ComparisonOperator GetComparison(string enumValue)
        {
            foreach (var field in ComparisonMembers)
            {
                if(field.GetCustomAttributes<EnumMemberAttribute>().First().Value == enumValue)
                {
                    return (ComparisonOperator)field.GetRawConstantValue();
                }
            }
            throw new InvalidOperationException("Unable to handle that value");
        }

        public override bool CanConvert(Type objectType)
        {
            return ExpressionType.IsAssignableFrom(objectType);
        }
    }
}