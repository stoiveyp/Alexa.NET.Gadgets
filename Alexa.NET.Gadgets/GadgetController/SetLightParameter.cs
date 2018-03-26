﻿using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Alexa.NET.Gadgets.GadgetController
{
    public class SetLightParameter
    {
        [JsonProperty("triggerEvent")]
        public string TriggerEvent { get; set; }

        [JsonProperty("triggerEventTimeMs")]
        public int TriggerEventTimeMilliseconds { get; set; }

        [JsonProperty("animations")]
        public List<SetLightAnimation> Animations { get; set; } = new List<SetLightAnimation>();

        public static SetLightParameter Create(string triggerEvent, int triggerMilliseconds,
            params SetLightAnimation[] animations)
        {
            return new SetLightParameter
            {
                TriggerEvent = triggerEvent,
                TriggerEventTimeMilliseconds = triggerMilliseconds,
                Animations = animations.ToList()
            };
        }
    }
}