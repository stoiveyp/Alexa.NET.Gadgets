using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Alexa.NET.Gadgets.CustomInterfaces
{
    public class CombinedFilterExpression : FilterExpression<CombinationOperator>
    {
        public CombinedFilterExpression() { }

        public CombinedFilterExpression(CombinationOperator op, params FilterExpression[] filters):base(op)
        {
            Filters = filters;
        }

        public FilterExpression[] Filters { get; set; }
        public override IEnumerable<object> ArrayItems()
        {
            return Filters;
        }
    }

    public enum CombinationOperator
    {
        [EnumMember(Value="and")]
        And,
        [EnumMember(Value = "or")]
        Or
    }
}