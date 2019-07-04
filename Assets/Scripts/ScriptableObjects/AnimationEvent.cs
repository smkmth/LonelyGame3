using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimationEvent : GameEventReceiver
{
    
    public string parameterToTrigger;
    public Animator animatorToActivate;

    public override void DoEvent()
    {
        animatorToActivate.SetTrigger(parameterToTrigger);
    }
}
