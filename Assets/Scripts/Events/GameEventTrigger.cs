using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// a non specific game event trigger, 
/// always fires when the player does the right action - has only one
/// condition
/// (the smart way to do this was to make player location a condition
/// in the conditional event trigger, but that would mean it was always
/// firing/checking)
/// </summary>
[AddComponentMenu("GameEvents/Game Event Trigger")]
public class GameEventTrigger : AbstractGameEventTrigger 
{
    
    private GameEventReceiver[] eventReceivers;

    public override void Start()
    {
        base.Start();
        eventReceivers = GetComponents<GameEventReceiver>();
       
    }

    public override void TriggerEvent()
    {


        if (!hasBeenTriggered || canTriggerAgain)
        {

            if (!triggersAfterTime || timerFinished)
            {
               
                foreach (GameEventReceiver receiver in eventReceivers)
                {
                    receiver.DoEvent();
                    if (deactivateSelfOnFinish)
                    {
                        gameObject.SetActive(false);
                    }

                }
                hasBeenTriggered = true;
            }
            else
            {
                timerStarted = true;

            }

        }

    }


}
