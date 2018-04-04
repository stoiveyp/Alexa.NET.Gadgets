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
response.GadgetColor("0000FF", new[] { "gadgetid1"}, 1000);
```
which is the same as
```
var setLight = new SetLightDirective
{
    TargetGadgets = new List<string>{"gadgetid1"},
    Parameters =
        SetLightParameter.Create(
            SetLightAnimation.CreateSingle(
				AnimationSegment.Create("0000FF", 1000)))
};

response?.Response?.Directives?.Add(setLight);

```
&nbsp;
## Set light with more explicit settings
```csharp
var setLight = new SetLightDirective
{
    TargetGadgets = new List<string> { "gadgetId1", "gadgetId2" },
    Parameters = new SetLightParameter
    {
        TriggerEvent = TriggerEvent.Down,
        TriggerEventTimeMilliseconds = 200,
        Animations = new List<SetLightAnimation> {
            new SetLightAnimation {
                Repeat = 1,
                TargetLights = new List<int> { 1 },
                Sequence = new List<AnimationSegment>
                        {
                            new AnimationSegment
                            {
                                Blend=false,
                                DurationMilliseconds = 3000,
                                Color="0000FF"
                            }
                        }
            }
        }
    }
};
skillResponse.Response.Directives.Add(setLight);
```