using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TriggerType
{
    OnEnterTriggerBox,
    OnExitTriggerBox,
    Interact,
    HoldInteract
}
public class GameEventTrigger : MonoBehaviour
{

    public TriggerType howEventIsTriggered;
    private GameEventReceiver[] eventReceivers;

    public bool canTriggerAgain = false;
    public bool hasBeenTriggered = false;

    public bool afterTime;
    public float timeToWait;
    private float timer;
    public bool timerStarted;

    public void Start()
    {

        eventReceivers = GetComponents<GameEventReceiver>();
        timer = 0.0f;
      
    }
    public void Update()
    {
        if (afterTime && (!hasBeenTriggered || canTriggerAgain))
        {
            if (timerStarted)
            {

                if (timer >= timeToWait)
                {
                    TriggerEvent();
                    timer = 0.0f;
                }
                else
                {
                    timer += Time.deltaTime;
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
   

        if (other.tag == "Player")
        {
            if (howEventIsTriggered == TriggerType.OnEnterTriggerBox)
            {
                if (!afterTime)
                {

                    TriggerEvent();
                }
                else
                {
                    timerStarted = true;
                }
            }


        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (howEventIsTriggered == TriggerType.OnExitTriggerBox)
            {
                if (!afterTime)
                {

                    TriggerEvent();
                }
                else
                {
                    timerStarted = true;
                }
            }

        }

    }

    public void TriggerEvent()
    {
        if (!hasBeenTriggered || canTriggerAgain)
        {

            foreach (GameEventReceiver receiver in eventReceivers)
            {
                receiver.DoEvent();

            }
            hasBeenTriggered = true;
        }
    }


}
