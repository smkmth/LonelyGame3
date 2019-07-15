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
public abstract class AbstractGameEventTrigger : MonoBehaviour
{
    public abstract void TriggerEvent();

    public TriggerType howEventIsTriggered;

    public bool canTriggerAgain = false;
    public bool hasBeenTriggered = false;

    public bool afterTime;
    public float timeToWait;
    public float timeToHold;
    private float timer;

    public bool deactivateSelfOnFinish;

    public bool timerStarted;
    public bool timerFinished;
    public bool lookedAt;

    public bool locked;

    Collider objCollider;
    Camera cam;
    Plane[] planes;


    public virtual void Start()
    {
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
            if (afterTime == false || timeToWait <= 0)
            {
                Debug.LogError("To Do TriggerBox stay, you need to mark AfterTime as true, and the time to wait as greater that zero, " +
                    "this is not true for " + gameObject.name);
            }
        }
        timer = 0.0f;

    }
    public void Update()
    {
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
        if (afterTime && (!hasBeenTriggered || canTriggerAgain))
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
