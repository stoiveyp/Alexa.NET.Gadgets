# Alexa.NET.Gadgets
A simple skill built to work with the Alexa Gadgets API, using Alexa.NET as the core

## Add support for GameEngine requests


```csharp
new GadgetRequestHandler().AddToRequestConverter();
```
&nbsp;
## Set light color of gadget
```csharp
using Alexa.NET.Gadgets
...
response.GadgetColor("0000FF", new[] { "gadgetid1"});
```
&nbsp;
## Set light with advanced settings
```csharp
var setLight = new SetLightDirective
{
    TargetGadgets = new List<string> { "gadgetId1", "gadgetId2" },
    Parameters = new SetLightParameter
    {
        TriggerEvent = TriggerEvent.None,
        TriggerEventTimeMilliseconds = 0,
        Animations = new List<SetLightAnimation> {
            new SetLightAnimation {
                Repeat = 1,
                TargetLights = new List<int> { 1 },
                Sequence = new List<AnimationSegment>
                        {
                            new AnimationSegment
                            {
                                Blend=false,
                                DurationMilliseconds = 10000,
                                Color="0000FF"
                            }
                        }
            }
        }
    }
};
skillResponse.Response.Directives.Add(setLight);
```