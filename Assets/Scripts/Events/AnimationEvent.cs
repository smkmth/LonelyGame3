using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationEventType
{
    Trigger,
    BoolTrue,
    BoolFalse
}
/// <summary>
/// an animation event calls a parameter on an animator when 
/// the event is triggered
/// </summary>
[AddComponentMenu("GameEvents/Animation Event")]
public class AnimationEvent : GameEventReceiver
{
    public AnimationEventType thisEventType;

    public string parameterToTrigger;
    public Animator animatorToActivate;

    public override void DoEvent()
    {
        switch (thisEventType)
        {
            case AnimationEventType.Trigger:
                animatorToActivate.SetTrigger(parameterToTrigger);
                break;
            case AnimationEventType.BoolTrue:
                animatorToActivate.SetBool(parameterToTrigger, true);
                break;
            case AnimationEventType.BoolFalse:
                animatorToActivate.SetBool(parameterToTrigger, false);
                break;
        }
    }
}
