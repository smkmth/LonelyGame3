using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeConditionEvent : GameEventReceiver
{
  
    public ConditionalGameEventTrigger gameEventToFlip;

   
    public override void DoEvent()
    { 
        if (gameEventToFlip.conditionIsTrue)
        {
            gameEventToFlip.conditionIsTrue = false;

        }
        else
        {
            gameEventToFlip.conditionIsTrue = true;

        }
    }
}
