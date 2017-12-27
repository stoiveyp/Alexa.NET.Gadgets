using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.Response;
using Newtonsoft.Json;

namespace Alexa.NET.Gadgets.GadgetController
{
    public class SetLightDirective : IDirective
    {
        [JsonProperty("type")] public string Type => "GadgetController.SetLight";

        [JsonProperty("version")] public int Version => 1;

        [JsonProperty("targetGadgets")] public List<string> TargetGadgets { get; set; } = new List<string>();

        [JsonProperty("parameters")]
        public SetLightParameter Parameters { get; set; }
    }
}
