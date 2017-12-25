using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.Response.Directive;
using Newtonsoft.Json;

namespace Alexa.NET.Response.Directive
{
    public class DeviationRecognizer : IGadgetRecognizer
    {
        [JsonProperty("type")]
        public string Type => "deviation";

        [JsonProperty("recognizer")]
        public string Recognizer { get; set; }
    }
}
