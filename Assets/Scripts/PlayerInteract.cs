using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public enum HoldState
{
    notHoldingItem,
    delay,
    holdingItem,
    putItemBack,
    itemBehindWall

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
    Interact,
    Unknown,
    HoldInteract
}
public class PlayerInteract : MonoBehaviour
{
    public float lookRadius;
    public float lookRange;
    public float maxLookAngle;
    public TextMeshProUGUI itemPrompt;
    public LayerMask blockingLayer;
    public LayerMask interactLayer;
    public InGameTextReader reader;
    float distanceToObject;
    public float readDistance;
    public float pickUpDistance;

    public Transform heldObject;
    public HoldState currentHoldState;
    public SmoothMouseLook smoothMouseLook;

    public LayerMask wallLayer;
    public Item heldItemData;
    public float pickUpRange;
    public float pickUpRadius;
    public Transform itemHeldTarget;
    public Transform inspectItemTarget;
    public float throwForce;
    public float holdDelay;
    public float holdTimer;
    public float maxInteractAngle;
    public float putBackDistance;
    public float distanceToHeldObject;

    public bool canPutBack;
    public bool holdingObject = false;
    public bool objectBehindWall;
    public bool isHoldingEndGameItem;

    public GameObject detectedObj;
    public ItemTypes thisItemType;
    private InGameCamera inGameCamera;

    public void Start()
    {
        reader = GetComponent<InGameTextReader>();
        currentHoldState = HoldState.notHoldingItem;
        inGameCamera =Camera.main.gameObject.GetComponent<InGameCamera>();
    }

   
    public void Update()
    {

        detectedObj = DetectObject();
        thisItemType = CheckDetectedObject(detectedObj);
        if (detectedObj != null)
        {
            distanceToObject = Vector3.Distance(detectedObj.transform.position, transform.position);

        }
        switch (thisItemType)
        {
            case ItemTypes.Book:
                itemPrompt.text = "Press E To Read";
                if (distanceToObject < readDistance)
                {

                    if (Input.GetButtonDown("Interact"))
                    {
                        reader.DisplayText(detectedObj.GetComponent<InGameTextObj>().textAsset);
                    }
                }

                break;
            case ItemTypes.Pickup:
                if (currentHoldState == HoldState.notHoldingItem)
                {
                    itemPrompt.text = "Press E To PickUp " + detectedObj.name;
                    if (Input.GetButtonDown("Interact"))
                    {
                        PickUp(detectedObj);
                    }
                }
                break;
            case ItemTypes.Film:
                itemPrompt.text = "Press E To PickUp Film";
                if (Input.GetButtonDown("Interact"))
                {
                    inGameCamera.cameraShots += 3;
                    Destroy(detectedObj);
                }
                break;

            case ItemTypes.Camera:
                itemPrompt.text = "Press E To PickUp Camera";
                if (Input.GetButtonDown("Interact"))
                {

                    inGameCamera.playerHasCamera = true;
                    Destroy(detectedObj);
                }
                break;
            case ItemTypes.Interact:
                itemPrompt.text = "Press E To Interact";
                if (Input.GetButtonDown("Interact"))
                {
                    detectedObj.GetComponent<GameEventTrigger>().TriggerEvent();

                }
                break;
            case ItemTypes.HoldInteract:

                itemPrompt.text = "Hold E To Interact";

                if (Input.GetButtonDown("Interact"))
                {

                    holdTimer = detectedObj.GetComponent<GameEventTrigger>().holdTimer;


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
                itemPrompt.text = "";
                break;

            case ItemTypes.Unknown:
                itemPrompt.text = "";
                break;

        }



        switch (currentHoldState)
        {
            case HoldState.notHoldingItem:
                break;
            case HoldState.holdingItem:
                break;
            case HoldState.delay:
                if (holdTimer > 0)
                {
                    holdTimer -= Time.deltaTime;

                }
                else
                {
                    holdTimer = holdDelay;
                    currentHoldState = HoldState.holdingItem;
                }
                break;
      
        }
    }

    public GameObject DetectObject()
    {
        //   Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward, Color.blue, 200.0f);

        RaycastHit hit;
        if (Physics.SphereCast(Camera.main.transform.position, lookRadius, Camera.main.transform.forward, out hit, lookRange, interactLayer))
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

        if(obj.tag == "Candle")
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
            itemPrompt.text = "Press E To PickUp Camera";

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
            itemPrompt.text = "Press E To Interact";

            return ItemTypes.Interact;
        }
        if (obj.tag == "EndGameItem")
        {

        }
        if (obj.tag == "HoldInteract")
        {
            itemPrompt.text = "Hold E To Interact";


            if (Input.GetButton("Interact"))
            {
                itemPrompt.text = "Working...";


            }
            return ItemTypes.HoldInteract;

        }
        return ItemTypes.Unknown;

    }

    public void PickUp(GameObject itemToHold)
    {
        
        currentHoldState = HoldState.delay;
        heldObject = itemToHold.transform;
        heldObject.parent = null;
        if (heldObject.name == "EndGameObject")
        {
            isHoldingEndGameItem = true;
        }
        heldObject.GetComponent<Rigidbody>().isKinematic = true;
        distanceToHeldObject = Vector3.Distance(transform.position, heldObject.position);
        heldItemData = heldObject.GetComponent<Item>();
        heldObject.GetComponent<Collider>().isTrigger = true;
        heldObject.position = itemHeldTarget.position;
        heldObject.rotation = itemHeldTarget.rotation;
        heldObject.parent = itemHeldTarget;
        holdingObject = true;
        ;

    }
    public void PutDown()
    {
        if (heldObject.name == "EndGameObject")
        {
            isHoldingEndGameItem = false;
        }
        heldObject.GetComponent<Collider>().isTrigger = false;
        heldItemData = null;
        canPutBack = false;
        heldObject = null;
        holdingObject = false;
        currentHoldState = HoldState.notHoldingItem;


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
}
