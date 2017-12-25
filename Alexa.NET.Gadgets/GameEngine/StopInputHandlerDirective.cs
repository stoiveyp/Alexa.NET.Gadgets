using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.Response;
using Newtonsoft.Json;

namespace Alexa.NET.Response.Directive
{
    public class StopInputHandlerDirective:IDirective
    {
        [JsonProperty("type")] public string Type => "GameEngine.StopInputHandler";
        [JsonProperty("originatingRequestId")]public string OriginatingRequestId { get; set; }
    }
}
