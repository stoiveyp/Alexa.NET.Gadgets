using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.Response;

namespace Alexa.NET.Gadgets.GadgetController
{
    public static class GadgetControllerExtensions
    {
        public static void GadgetColor(this SkillResponse response, string color, IEnumerable<string> gadgetIds, int durationMilliseconds = 1000)
        {
            var setLight = new SetLightDirective
            {
                TargetGadgets = new List<string>(gadgetIds),
                Parameters =
                    SetLightParameter.Create(
                        SetLightAnimation.CreateSingle(AnimationSegment.Create(color, durationMilliseconds)))
            };

            response?.Response?.Directives?.Add(setLight);
        }
    }
}
