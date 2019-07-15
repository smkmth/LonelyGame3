using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



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

            if (!afterTime || timerFinished)
            {
                if (!locked)
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
             
            }
            else
            {
                timerStarted = true;

            }

        }

    }


}
