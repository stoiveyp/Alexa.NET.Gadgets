using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Alexa.NET.Gadgets.CustomInterfaces
{
    [JsonConverter(typeof(FilterExpressionConverter))]
    public abstract class FilterExpression<T>:FilterExpression
    {
        protected FilterExpression() { }

        protected FilterExpression(T op)
        {
            Operator = op;
        }

        internal override string GetOperator()
        {
            var attrib = (EnumMemberAttribute) typeof(T).GetMember(Operator.ToString()).Last()
                .GetCustomAttributes(typeof(EnumMemberAttribute), false).First();

            return attrib.Value;
        }

        public T Operator { get; set; }
    }

    public abstract class FilterExpression
    {
        internal abstract string GetOperator();
        public abstract IEnumerable<object> ArrayItems();
    }
}