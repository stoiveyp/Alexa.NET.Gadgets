using System.Collections.Generic;
using Alexa.NET.Gadgets.GameEngine;
using Xunit;

namespace Alexa.NET.Gadgets.Tests
{
    public class RecognizerTests
    {
        [Fact]
        public void ProgressRecognizerSerializesCorrectly()
        {
            var recognizer = new ProgressRecognizer
            {
                Recognizer = "patternMatcher",
                Completion=50
            };
            Assert.True(Utility.CompareJson(recognizer,"ProgressRecognizer.json"));
        }

        [Fact]
        public void DeviationRecognizerSerializesCorrectly()
        {
            var recogniser = new DeviationRecognizer
            {
                Recognizer = "recognizerName"
            };
            Assert.True(Utility.CompareJson(recogniser,"DeviationRecognizer.json"));
        }

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
