using System.Runtime.Serialization;

namespace Alexa.NET.Gadgets.CustomInterfaces
{
    public enum FilterMatchAction
    {
        [EnumMember(Value="SEND")]
        Send,
        [EnumMember(Value="SEND_AND_TERMINATE")]
        SendAndTerminate
    }
}