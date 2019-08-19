using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace Alexa.NET.Gadgets.CustomInterfaces
{
    public class ComparisonFilterExpression:FilterExpression<ComparisonOperator>
    {
        public ComparisonFilterExpression() { }
        public ComparisonFilterExpression(ComparisonOperator op, string variable, object value) : base(op)
        {
            Variable = variable;
            Value = value;
        }

        public object Value { get; set; }

        public string Variable { get; set; }
        public override IEnumerable<object> ArrayItems()
        {
            return new[] {new VariableContainer {Variable = Variable}, Value};
        }
    }

    internal class VariableContainer
    {
        [JsonProperty("var")]
        public string Variable { get; set; }
    }

    public enum ComparisonOperator
    {
        [EnumMember(Value="==")]
        Equals,
        [EnumMember(Value = "===")]
        StrictEquals,
        [EnumMember(Value = "!=")]
        NotEquals,
        [EnumMember(Value = "!==")]
        StrictNotEquals,
        [EnumMember(Value = ">")]
        GreaterThan,
        [EnumMember(Value = ">=")]
        GreaterThanEquals,
        [EnumMember(Value = "<")]
        LessThan,
        [EnumMember(Value = "<=")]
        LessThanEquals
    }
}
