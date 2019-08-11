using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// an animation event calls a parameter on an animator when 
/// the event is triggered
/// </summary>
[AddComponentMenu("GameEvents/Animation Event")]
public class AnimationEvent : GameEventReceiver
{
    
    public string parameterToTrigger;
    public Animator animatorToActivate;

    public override void DoEvent()
    {
        animatorToActivate.SetTrigger(parameterToTrigger);
    }
}
