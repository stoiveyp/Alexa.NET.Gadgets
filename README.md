# Alexa.NET.Gadgets
A simple skill built to work with the Alexa Gadgets API, using Alexa.NET as the core

# Game Controller Support

## Set all attached buttons to the same color
A good one to show the user active buttons during roll call
```csharp
using Alexa.NET.Gadgets.GadgetController
...
response.GadgetColor("0000FF",10000)
```
which is the same as
```csharp
var setLight = new SetLightDirective
{
    Parameters =
        SetLightParameter.Create(
            SetLightAnimation.CreateSingle(
				AnimationSegment.Create("0000FF", 10000)))
};

response.Response.Directives.Add(setLight);
```
## Set light color of a specific gadget
```csharp
using Alexa.NET.Gadgets.GadgetController
...
response.GadgetColor("0000FF", new[] { "gadgetid1"}, 10000);
```
which is the same as
```csharp
var setLight = new SetLightDirective
{
    TargetGadgets = new List<string>{"gadgetid1"},
    Parameters =
        SetLightParameter.Create(
            SetLightAnimation.CreateSingle(
				AnimationSegment.Create("0000FF", 10000)))
};

response.Response.Directives.Add(setLight);

```

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

# Game Engine Support
## Add support for GameEngine requests - please this in constructor/startup
```csharp
new GadgetRequestHandler().AddToRequestConverter();
```

## Roll Call - Ask User to identify buttons

Adds a directive to the response that looks for buttons to be pressed, and assigns them to the list passed in on a first come first served basis (Good to use in combination with a general GadgetColor from GameController to show the user which buttons they can pick from)

```csharp
using Alexa.NET.Gadgets.GameEngine
...
response.AddRollCall("first", "second");
```
This is equivalent to:
  -  A StartInputHandler Directive
    - Proxy added for each name passed in
    - A timed out event
    - A "rollcall complete" event
    - A "rollcall complete" recogniser
      - A pattern for each name to be pressed down, once, in order. 

## Roll Call - Find mandatory gadgets used in roll call
When an InputHandlerEventRequest is identified, this will check to see if it was a rollcall event, and then map the matched events to the gadget ids in the order passed in. If not all gadgets are pressed down within the time it will return false.

Assuming the previous AddRollCall method was used, this would return a dictionary mapping first -> firstGadgetId, second -> secondGadgetId
```csharp
using Alexa.NET.Gadgets.GameEngine
...
switch(skillRequest.Request)
{
    case InputHandlerEventRequest inputHandler:
      inputHandler.TryRollCallResult(out Dictionary<string,string> mapping, "first","second");
}
```


## Roll Call - Find optional gadgets used in roll call
When an InputHandlerEventRequest is identified, this will check to see if it was a rollcall event, and then map the matched events to the gadget ids in the order passed in. If not all gadgets are passed in, it will return a mapping for those distinct buttons pressed during the time. If the event was neither roll call nor time out, or no event was found, then it will return false.

Assuming the previous AddRollCall method was used, this would return:
- a dictionary mapping first -> firstGadgetId, second -> secondGadgetId if both buttons were pressed
- a dictionary mapping first -> firstGadgetId if one button was pressed within the timeout
- a false result and a null mapping object if the event was neither rollcall nor timeout
```csharp
using Alexa.NET.Gadgets.GameEngine
...
switch(skillRequest.Request)
{
    case InputHandlerEventRequest inputHandler:
      inputHandler.TryRollCallOptionalResult(out Dictionary<string,string> mapping, "first","second");
}
```
