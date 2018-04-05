using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.Response;

namespace Alexa.NET.Gadgets.GadgetController
{
    public static class GadgetControllerExtensions
    {
        public static SetLightDirective GadgetColor(this SkillResponse response, string color, int durationMilliseconds = 1000)
        {
            return GadgetColor(response, color, null, durationMilliseconds);
        }

        public static SetLightDirective GadgetColor(this SkillResponse response, string color, IEnumerable<string> gadgetIds, int durationMilliseconds = 1000)
        {
            var setLight = new SetLightDirective
            {
                TargetGadgets = gadgetIds == null ? null : new List<string>(gadgetIds),
                Parameters =
                    SetLightParameter.Create(
                        SetLightAnimation.CreateSingle(AnimationSegment.Create(color, durationMilliseconds)))
            };

            SetDirective(response, setLight);

            return setLight;
        }

        private static void SetDirective(SkillResponse response, IDirective directive)
        {
            if (response == null)
            {
                throw new InvalidOperationException("Unable to set gadget colors on null response");
            }

            if (response.Response == null)
            {
                response.Response = new ResponseBody();
            }

            if (response.Response.Directives == null)
            {
                response.Response.Directives = new List<IDirective>();
            }
            response.Response.Directives.Add(directive);
        }
    }
}
