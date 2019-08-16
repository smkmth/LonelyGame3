using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TriggerType
{
    OnEnterTriggerBox,
    OnExitTriggerBox,
    OnTriggerBoxStay,
    Interact,
    HoldInteract,
    LookAtTriggerBox,
    LookAwayFromTriggerBox,
    NeverTrigger
}

/// <summary>
/// Top level game event trigger. When the condition for a game trigger is met, 
/// the game event trigger steps through all the game event recivers attached to 
/// the game event trigger objects and calls a method DoEvent()
/// </summary>
public abstract class AbstractGameEventTrigger : MonoBehaviour
{

 
    public abstract void TriggerEvent();

    public TriggerType howEventIsTriggered;

    public bool canTriggerAgain = false;
    public bool hasBeenTriggered = false;

    [Header("Make this event fire after an amount of time has passed")]
    public bool triggersAfterTime;
    public float timeToWait;


    public float timeToHold;
    public float timer;

    [Header("After the event, turn this gameObject off")]
    public bool deactivateSelfOnFinish;

    [HideInInspector]
    public bool timerStarted;
    [HideInInspector]
    public bool timerFinished;
    [HideInInspector]
    public bool lookedAt;


    Collider objCollider;
    Camera cam;
    Plane[] planes;


    public virtual void Start()
    {
        //Some setup, i do this here so we dont have to manually do this for all the 
        //objects individually 

        if (howEventIsTriggered == TriggerType.LookAwayFromTriggerBox)
        {
            cam = Camera.main;
            objCollider = GetComponent<Collider>();
        }
        if (howEventIsTriggered == TriggerType.LookAtTriggerBox)
        {
            tag = "LookAt";
        }
        if (howEventIsTriggered == TriggerType.OnTriggerBoxStay)
        {
            if (triggersAfterTime == false || timeToWait <= 0)
            {
                Debug.LogError("To Do TriggerBox stay, you need to mark AfterTime as true, and the time to wait as greater that zero, " +
                    "this is not true for " + gameObject.name);
            }
        }

        timer = 0.0f;

    }
    public void Update()
    {
        //This is the looked at event, we use a specific camera frustrum and the testplanesaabb here
        //so the editor camera doesnt get in the way

        if (howEventIsTriggered == TriggerType.LookAwayFromTriggerBox)
        {
            if (lookedAt)
            {
                planes = GeometryUtility.CalculateFrustumPlanes(cam);
                if (GeometryUtility.TestPlanesAABB(planes, objCollider.bounds))
                {

                }
                else
                {
                    TriggerEvent();
                }
            }
        }
        if (triggersAfterTime && (!hasBeenTriggered || canTriggerAgain))
        {
            if (timerStarted)
            {

                if (timer >= timeToWait)
                {
                    TriggerEvent();
                    timerFinished = true;
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
                TriggerEvent();
            }
            else if (howEventIsTriggered == TriggerType.OnTriggerBoxStay)
            {
                TriggerEvent();

            }



        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (howEventIsTriggered == TriggerType.OnExitTriggerBox)
            {
                TriggerEvent();
            }
            else if (howEventIsTriggered == TriggerType.OnTriggerBoxStay)
            {
                timerStarted = false;
                hasBeenTriggered = true;

            }

        }

    }
}
