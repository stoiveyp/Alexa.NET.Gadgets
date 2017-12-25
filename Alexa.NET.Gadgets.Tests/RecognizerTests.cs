using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.Response.Directive;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Alexa.NET.Gadgets.Tests
{
    public class RecognizerTests
    {
        [Fact]
        public void PatternRecognizerSerializesCorrectly()
        {
            var recogniser = new PatternRecognizer
            {
                Anchor = PatternRecognizerAnchor.End,
                Fuzzy = true,
                GadgetIds = new List<string> { "gadgetId1","gadgetId2","gadgetId3"},
                Actions = new List<string> { "down","up"},
                Patterns = new List<Pattern>
                {
                    new Pattern
                    {
                        Action="down",
                        GadgetIds = new List<string>{"gadgetId1","gadgetId2"},
                        Colors = new List<string>{"0000FF"}
                    }
                }
            };
            
            Assert.True(Utility.CompareJson(recogniser,"PatternRecognizer.json"));
        }
    }
}
