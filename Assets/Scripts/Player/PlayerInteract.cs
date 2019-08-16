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
    public TextDisplayer textDisplayer;
    private InGameTextReader reader;
    private PlayerManager manager;
    public GameObject cameraObj;                        //players main camera tagged as main camera, got from Camera.main
    private Inventory inv;

    [Header("Player View")]
    public LayerMask blockingLayer;
    public LayerMask interactLayer;
    public LayerMask wallLayer;
    public float lookRadius;
    public float lookRange;
    public float maxLookAngle;
     public GameObject detectedObj;
    public TextMeshProUGUI itemPrompt;


     public float distanceToObject;

    public Image curser;
    public Color interactColor;
    public Color noColor;
    public Transform itemHeldTarget;
    public Transform inspectItemTarget;

    public float readDistance;
    public float pickUpDistance;

    private Transform heldObject;
    public HoldState currentHoldState;
    public float throwForce;

    public float holdStateDelay;
     public float holdTimer;
     public float distanceToHeldObject;

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

    [Header("Player Audio")]
    public AudioSource source;
    public AudioClip paperPickup;
    public AudioClip objectPickup;
    public float pickupVol;
    public Item endGameItem;

    public void Start()
    {
        source = GetComponent<AudioSource>();
        cameraObj = Camera.main.gameObject;
        textDisplayer = GetComponent<TextDisplayer>();
        manager = GetComponent<PlayerManager>();
        reader = GetComponent<InGameTextReader>();
        currentHoldState = HoldState.notHoldingItem;
        inGameCamera =Camera.main.gameObject.GetComponent<InGameCamera>();
        controller = GetComponent<FirstPersonCharacterController>();
        inv = GetComponent<Inventory>();
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
  

    public void AddToInventory(GameObject gameObjectPickUp)
    {
        itemPrompt.text = "";
        curser.color = noColor;

        Item item = gameObjectPickUp.GetComponent<ItemContainer>().heldItem;
        switch (item.type)
        {
            case ItemType.Book:
                source.PlayOneShot(paperPickup, pickupVol);
                InGameText book = (InGameText)item;
                textDisplayer.AddTextAsset(book);
                reader.DisplayText(book, false);
                break;
            default:
                source.PlayOneShot(objectPickup, 1.0f);
                inv.AddItem(item);
                break;
        }
        gameObjectPickUp.GetComponent<ItemContainer>().PickUpItem();

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
        curser.color = noColor;
        itemPrompt.text = "";

        if (detectedObj == null)
        {

            return;
        }
  
        if (thisItemType == ItemTypes.LookAtObj)
        {
            GameEventTrigger lookatTrigger = detectedObj.GetComponent<GameEventTrigger>();
            if (lookatTrigger)
            {
                if (lookatTrigger.howEventIsTriggered == TriggerType.LookAwayFromTriggerBox)
                {
                    lookatTrigger.lookedAt = true;
                    lookingAt = true;
                    lookAtEventObj = lookatTrigger;
                }
                else if (lookatTrigger.howEventIsTriggered == TriggerType.LookAtTriggerBox)
                {
                   lookatTrigger.TriggerEvent();

                }
            }
            else
            {
                Debug.LogError("No GameEventTrigger on LookAtObj");
            }
        }

        curser.color = noColor;
        AbstractGameEventTrigger itemtrigger = null;
        if (thisItemType != ItemTypes.Wall || thisItemType != ItemTypes.Surface || thisItemType != ItemTypes.Unknown || thisItemType != ItemTypes.Nothing)
        {
            itemtrigger = detectedObj.GetComponent<AbstractGameEventTrigger>();
        }
        if (distanceToObject <= pickUpDistance)
        {
            switch (thisItemType)
            {
                
                case ItemTypes.Book:
                    itemPrompt.text = "Press <sprite=0> to Read";
                    curser.color = interactColor;
                    if (Input.GetButtonDown("Interact"))
                    {

                        if (itemtrigger)
                        {
                            
                            itemtrigger.TriggerEvent();
                        }
                        AddToInventory(detectedObj);
                        return;
                    }

                    break;
                case ItemTypes.Pickup:
                   
                        curser.color = interactColor;

                        itemPrompt.text = "Press <sprite=0> to pick up " + detectedObj.name;
                        if (Input.GetButtonDown("Interact"))
                        {
                            
                            if (itemtrigger)
                            {
                                itemtrigger.TriggerEvent();
                            }
                            AddToInventory(detectedObj);
                        }
                        
                       
                    
                    break;
                case ItemTypes.Film:

                    curser.color = interactColor;

                    itemPrompt.text = "Press <sprite=0> to insert/remove crank";
                    if (Input.GetButtonDown("Interact"))
                    {

                        detectedObj.GetComponent<AbstractGameEventTrigger>().TriggerEvent();
                        return;

                    }
                    break;

                case ItemTypes.Camera:
                    curser.color = interactColor;

                    itemPrompt.text = "Press <sprite=0> to operate crank";
                    if (Input.GetButtonDown("Interact"))
                    {

                        detectedObj.GetComponent<AbstractGameEventTrigger>().TriggerEvent();
                        return;

                    }
                    break;
                case ItemTypes.Interact:

                    curser.color = interactColor;

                    itemPrompt.text = "Press <sprite=0> to Interact";
                    if (Input.GetButtonDown("Interact"))
                    {
                      
                        detectedObj.GetComponent<AbstractGameEventTrigger>().TriggerEvent();
                        return;

                    }
                    break;
                case ItemTypes.HoldInteract:


                    if (inv.GetItemCount(endGameItem) > 0)
                    {

                        curser.color = interactColor;
                        itemPrompt.text = "Press <sprite=0> to perform ritual prayer";
                        if (Input.GetButtonDown("Interact"))
                        {

                            detectedObj.GetComponent<AbstractGameEventTrigger>().TriggerEvent();
                            return;

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

   
    }
}
