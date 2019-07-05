using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum HoldState
{
    notHoldingItem,
    delay,
    holdingItem,
    putItemBack,
    inspectingItem,
    itemBehindWall,

}
public enum ItemTypes
{
    Nothing,
    Wall,
    Pickup,
    Surface,
    Film,
    Camera,
    Book,
    LookAtObj,
    Interact,
    Unknown,
    HoldInteract
}
public class PlayerInteract : MonoBehaviour
{
    private InGameTextReader reader;
    private PlayerManager manager;
    public GameObject cameraObj;                        //players main camera tagged as main camera, got from Camera.main

    [Header("Player View")]
    public LayerMask blockingLayer;
    public LayerMask interactLayer;
    public LayerMask wallLayer;
    public float lookRadius;
    public float lookRange;
    public float maxLookAngle;
    [ReadOnly] public GameObject detectedObj;
    public TextMeshProUGUI itemPrompt;


    [ReadOnly] public float distanceToObject;

    [Header("Holding Object")]
    public Image curser;
    public Color interactColor;
    public Color noColor;
    public Transform itemHeldTarget;
    public Transform inspectItemTarget;

    public float readDistance;
    public float pickUpDistance;

    private Transform heldObject;
    [ReadOnly] public HoldState currentHoldState;
    public float throwForce;

    public float holdStateDelay;
    [ReadOnly] public float holdTimer;
    [ReadOnly] public float distanceToHeldObject;

    public bool isHoldingEndGameItem;

    //public ItemTypes thisItemType;
    private InGameCamera inGameCamera;
    public HoldState nextHoldState;
    public float inspectRotSpeed;
    public FirstPersonCharacterController controller;
    public bool interactIsActive =true;

    public Image crosshair;

    //LookAt event stuff
    public GameEventTrigger lookAtEventObj;
    public bool lookingAt;


    public void Start()
    {
        cameraObj = Camera.main.gameObject;
        
        manager = GetComponent<PlayerManager>();
        reader = GetComponent<InGameTextReader>();
        currentHoldState = HoldState.notHoldingItem;
        inGameCamera =Camera.main.gameObject.GetComponent<InGameCamera>();
        controller = GetComponent<FirstPersonCharacterController>();
    }

   
    public void Update()
    {
        if (interactIsActive)
        {

            detectedObj = DetectObject();
            ItemTypes selectedItemType = CheckDetectedObject(detectedObj);
            if (detectedObj != null)
            {
                distanceToObject = Vector3.Distance(detectedObj.transform.position, transform.position);

            }
            CheckCurser(selectedItemType);

            switch (currentHoldState)
            {
                case HoldState.notHoldingItem:
                    break;
                case HoldState.holdingItem:

                    if (Input.GetButton("InspectItem"))
                    {
                        ChangeHoldState(HoldState.inspectingItem);
                    }
                    break;
                case HoldState.delay:
                    InputDelay();
                    break;
                case HoldState.inspectingItem:
                    if (!Input.GetButton("InspectItem"))
                    {
                        ChangeHoldState(HoldState.holdingItem, false);

                    }
                    heldObject.Rotate(Vector3.up, Input.GetAxis("Mouse X") * inspectRotSpeed * Time.deltaTime);
                    break;

            }
        }
    }

  
    public void InputDelay()
    {
        if (holdTimer > 0)
        {
            holdTimer -= Time.deltaTime;

        }
        else
        {
            holdTimer = holdStateDelay;
            currentHoldState = nextHoldState;
        }
    } 


    public void ChangeHoldState(HoldState newHoldState, bool holdDelay = true)
    {
      //  Debug.Log("current hold state is " + currentHoldState + " new hold state is " + newHoldState);

        switch (newHoldState)
        {
            case HoldState.holdingItem:
                heldObject.transform.position = itemHeldTarget.position;
                manager.ChangePlayerState(PlayerState.freeMovement);

                break;
            case HoldState.inspectingItem:
                heldObject.transform.position = inspectItemTarget.position;
                manager.ChangePlayerState(PlayerState.inspectMode);

                break;
            case HoldState.notHoldingItem:
                controller.characterIsActive = true;
                manager.ChangePlayerState(PlayerState.freeMovement);

                break;
            
        }
        if (holdDelay)
        {
            nextHoldState = newHoldState;
            currentHoldState = HoldState.delay;

        }
        else
        {
            currentHoldState = newHoldState;

        }
    }
  

 

    public void PickUp(GameObject itemToHold)
    {
        heldObject = itemToHold.transform;
        heldObject.parent = null;
        if (heldObject.name == "EndGameObject")
        {
            isHoldingEndGameItem = true;
        }
        heldObject.gameObject.layer = LayerMask.NameToLayer("HeldItem");
        heldObject.GetComponent<Rigidbody>().isKinematic = true;
        distanceToHeldObject = Vector3.Distance(transform.position, heldObject.position);
        heldObject.GetComponent<Collider>().isTrigger = true;
        heldObject.position = itemHeldTarget.position;
        heldObject.rotation = itemHeldTarget.rotation;
        heldObject.parent = itemHeldTarget;
        
        ChangeHoldState(HoldState.holdingItem);
        

    }
    public void PutDown()
    {
        if (heldObject.name == "EndGameObject")
        {
            isHoldingEndGameItem = false; 
        }
        heldObject.gameObject.layer = LayerMask.NameToLayer("Item");
        heldObject.GetComponent<Collider>().isTrigger = false;
        heldObject = null;

        ChangeHoldState(HoldState.notHoldingItem, false);

    }

    public void Drop()
    {
        if (IsItemBehindWall())
        {
            return;
        }
        heldObject.GetComponent<Collider>().isTrigger = false;
        heldObject.GetComponent<Rigidbody>().isKinematic = false;
        heldObject.GetComponent<Rigidbody>().AddForce(cameraObj.transform.forward * throwForce, ForceMode.Impulse);
        heldObject.gameObject.layer = LayerMask.NameToLayer("Item");

        heldObject.parent = null;
        heldObject = null;
        ChangeHoldState(HoldState.notHoldingItem, false);


    }


    public bool IsItemBehindWall()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, (heldObject.position - transform.position), out hit, distanceToHeldObject, wallLayer))
        {
            return true;

        }
        else
        {

            return false;
        }
    }

    public GameObject DetectObject()
    {
        //Debug.DrawRay(cameraObj.transform.position, cameraObj.transform.forward, Color.blue, 200.0f);
        RaycastHit hit;
        if (Physics.SphereCast(cameraObj.transform.position, lookRadius, cameraObj.transform.forward, out hit, lookRange, interactLayer))
        {
            return hit.collider.gameObject;
        }

        return null;

    }

    public ItemTypes CheckDetectedObject(GameObject obj)
    {


        if (obj == null)
        {
            itemPrompt.text = "";
            return ItemTypes.Nothing;
        }
        if (obj.tag == "Blocking")
        {
            return ItemTypes.Wall;
        }

        if (obj.tag == "Candle")
        {

        }
        if (obj.tag == "Surface")
        {
            return ItemTypes.Surface;
        }

        if (obj.tag == "Pickup")
        {

            return ItemTypes.Pickup;
        }
        if (obj.tag == "Camera")
        {
            itemPrompt.text = "Press LMB to PickUp Camera";

            return ItemTypes.Camera;
        }
        if (obj.tag == "Film")
        {

            return ItemTypes.Film;

        }
        if (obj.tag == "TextObj")
        {


            return ItemTypes.Book;
        }
        if (obj.tag == "Interact")
        {
            itemPrompt.text = "Press LMB to Interact";

            return ItemTypes.Interact;
        }
        if (obj.tag == "EndGameItem")
        {
            if (Input.GetButtonDown("Interact"))
            {

                if (isHoldingEndGameItem)
                {
                    manager.WinGame();
                }
            }
        }
        if (obj.tag == "HoldInteract")
        {
            itemPrompt.text = "Hold LMB to Interact";


            if (Input.GetButton("Interact"))
            {
                itemPrompt.text = "Working...";


            }
            return ItemTypes.HoldInteract;


        }
        if (obj.tag == "LookAt")
        {
            return ItemTypes.LookAtObj;
        }
        return ItemTypes.Unknown;

    }

    private void CheckCurser(ItemTypes thisItemType)
    {
        /*
        if (lookingAt)
        {
            if (detectedObj.name != lookAtEventObj.name)
            {
                lookingAt = false;
                lookAtEventObj.TriggerEvent();
                lookAtEventObj = null;
            }
        }
        */
        if (thisItemType == ItemTypes.LookAtObj)
        {
            GameEventTrigger trigger = detectedObj.GetComponent<GameEventTrigger>();
            if (trigger)
            {
                if (trigger.howEventIsTriggered == TriggerType.LookAwayFromTriggerBox)
                {
                    trigger.lookedAt = true;
                    lookingAt = true;
                    lookAtEventObj = trigger;
                }
                else if (trigger.howEventIsTriggered == TriggerType.LookAtTriggerBox)
                {
                   trigger.TriggerEvent();

                }
            }
            else
            {
                Debug.LogError("No GameEventTrigger on LookAtObj");
            }
        }
        curser.color = noColor;
       
        GameEventTrigger trigger = detectedObj.GetComponent<GameEventTrigger>();
        if (distanceToObject <= pickUpDistance)
        {
            switch (thisItemType)
            {
                
                case ItemTypes.Book:
                    itemPrompt.text = "Press LMB to Read";
                    curser.color = interactColor;
                    if (Input.GetButtonDown("Interact"))
                    {

                        if (trigger)
                        {
                            trigger.TriggerEvent();
                        }
                        reader.DisplayText(detectedObj.GetComponent<InGameTextObj>().textAsset);
                        return;
                    }

                    break;
                case ItemTypes.Pickup:
                   

                    if (currentHoldState == HoldState.notHoldingItem)
                    {
                        curser.color = interactColor;

                        itemPrompt.text = "Press LMB to PickUp " + detectedObj.name;
                        if (Input.GetButtonDown("Interact"))
                        {
                            PickUp(detectedObj);
                        }
                        
                       
                    }
                    break;
                case ItemTypes.Film:

                    curser.color = interactColor;

                    itemPrompt.text = "Press LMB to PickUp Film";
                    if (Input.GetButtonDown("Interact"))
                    {

                        inGameCamera.filmCanisters += 1;
                        
                        Destroy(detectedObj);
                    }
                    break;

                case ItemTypes.Camera:

                    curser.color = interactColor;

                    itemPrompt.text = "Press LMB to PickUp Camera";
                    if (Input.GetButtonDown("Interact"))
                    {

                        trigger.TriggerEvent();
                        inGameCamera.UpdateShots(12);
                        inGameCamera.playerHasCamera = true;
                        inGameCamera.cameraIsActive = true;
                        inGameCamera.energyBar.gameObject.SetActive(true);
                        Destroy(detectedObj);
                    }
                    break;
                case ItemTypes.Interact:

                    curser.color = interactColor;

                    itemPrompt.text = "Press LMB to Interact";
                    if (Input.GetButtonDown("Interact"))
                    {

                        detectedObj.GetComponent<GameEventTrigger>().TriggerEvent();
                        return;

                    }
                    break;
                case ItemTypes.HoldInteract:

                    curser.color = interactColor;

                    itemPrompt.text = "Hold LMB to Interact";

                    if (Input.GetButtonDown("Interact"))
                    {




                    }
                    if (Input.GetButton("Interact"))
                    {
                        itemPrompt.text = "Working...";

                        holdTimer -= Time.deltaTime;

                        if (holdTimer < 0)
                        {
                            itemPrompt.text = "Done";

                            detectedObj.GetComponent<GameEventTrigger>().TriggerEvent();

                        }

                    }
                    break;
                case ItemTypes.Surface:
                    if (currentHoldState == HoldState.holdingItem)
                    {

                        curser.color = interactColor;
                        itemPrompt.text = "Put Down?";
                        if (Input.GetButtonDown("Interact"))
                        {

                            for (int i = 0; i < detectedObj.transform.childCount; i++)
                            {
                                Transform child = detectedObj.transform.GetChild(i);
                                if (child.tag == "ItemSlot")
                                {
                                    if (child.transform.childCount == 0)
                                    {
                                        heldObject.parent = child;
                                        heldObject.transform.position = child.transform.position;
                                        heldObject.transform.rotation = Quaternion.identity;
                                        PutDown();
                                        itemPrompt.text = "";
                                        break;

                                    }
                                }
                            }
                        }


                    }
                    break;
                case ItemTypes.Wall:
                    curser.color = noColor;

                    itemPrompt.text = "";
                    break;

                case ItemTypes.Unknown:

                    curser.color = noColor;
                    itemPrompt.text = "";
                    break;

            }
            

        }
        else
        {
            itemPrompt.text = "";
        }

        if (currentHoldState == HoldState.holdingItem)
        {
            if (Input.GetButtonDown("Interact"))
            { 
                Drop();
            }
        }
    }
}
