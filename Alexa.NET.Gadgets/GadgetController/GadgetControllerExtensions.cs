using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.Response;

namespace Alexa.NET.Gadgets.GadgetController
{
    public static class GadgetControllerExtensions
    {
        public static void GadgetColor(this SkillResponse response, string color, IEnumerable<string> gadgetIds, int durationMilliseconds = 1)
        {
            var setLight = new SetLightDirective
            {
                TargetGadgets = new List<string>(gadgetIds),
                Parameters = new SetLightParameter
                {
                    TriggerEvent = TriggerEvent.None,
                    TriggerEventTimeMilliseconds = 0,
                    Animations = new List<SetLightAnimation>{new SetLightAnimation
                        {
                            Repeat = 1,
                            TargetLights = new List<string> { "1" },
                            Sequence = new List<AnimationSegment>
                            {
                                new AnimationSegment
                                {
                                    Blend=false,
                                    DurationMilliseconds = durationMilliseconds,
                                    Color=color
                                }
                            }
                        }
                    }

                }
            };

            response?.Response?.Directives?.Add(setLight);
        }
    }
}
