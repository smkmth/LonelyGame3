using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/*
public class PickUpItem : MonoBehaviour {

    public SmoothMouseLook smoothMouseLook;
    public TextMeshProUGUI itemPrompt;

    public LayerMask wallLayer;
    private Transform heldObject = null;
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

    public HoldState currentHoldState;
    public InGameTextReader reader;
    public InGameCamera inGameCamera;

    // Use this for initialization
    void Start () {
        holdTimer = holdDelay;
        itemPrompt.gameObject.SetActive(true);
        itemPrompt.text = "";
        reader = GetComponent<InGameTextReader>();
        currentHoldState = HoldState.notHoldingItem;
    }


  
    public GameObject DetectObject(string tagToDetect)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.SphereCastAll(ray, pickUpRadius, pickUpRange);
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.tag == tagToDetect)
            {
                Vector3 angle = (hit.point- Camera.main.transform.position).normalized;
                RaycastHit testRay;
                if (Physics.Raycast(Camera.main.transform.position, angle.normalized, out testRay, 1000.0f))
                {
                    if (hit.collider.tag == tagToDetect)
                    {
                        float angleTo = Vector3.Angle(angle, Camera.main.transform.forward);
                        if (Mathf.Abs(angleTo) < maxInteractAngle)
                        {
                            return hit.collider.gameObject;
                        }
                    }
                }
            }
            

        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        /*

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.SphereCast(ray, 0.5f, out hit, pickUpRange))
        {
            if (hit.collider.tag == "Camera")
            {
                itemPrompt.text = "Press E To PickUp Camera";
                if (Input.GetButtonDown("Interact"))
                {

                    inGameCamera.playerHasCamera = true;
                    Destroy(hit.collider.gameObject);
                }
                return;
            }
            if (hit.collider.tag == "Film")
            {
                itemPrompt.text = "Press E To PickUp Film";
                if (Input.GetButtonDown("Interact"))
                {
                    
                    inGameCamera.cameraShots += 3;
                    Destroy(hit.collider.gameObject);
                }
                return;
            }
            if (hit.collider.tag == "TextObj")
            {
                itemPrompt.text = "Press E To Read";
                if (Input.GetButtonDown("Interact"))
                {
                    InGameText textObj = hit.collider.gameObject.GetComponent<InGameTextObj>().textAsset;
                    reader.DisplayText(textObj);

                }
                return;
            }
            if (hit.collider.tag == "Interact")
            {
                itemPrompt.text = "Press E To Interact"; 
                if (Input.GetButtonDown("Interact"))
                {
                    hit.collider.gameObject.GetComponent<GameEventTrigger>().TriggerEvent();

                }
                return;
            }
            if (hit.collider.tag == "EndGameItem")
            {
                if (isHoldingEndGameItem)
                {
                    itemPrompt.text = "Press E To Interact";
                    if (Input.GetButtonDown("Interact"))
                    {
                        hit.collider.gameObject.GetComponent<GameEventTrigger>().TriggerEvent();

                    }
                }
                return;
            }
            if (hit.collider.tag == "HoldInteract")
            {
                itemPrompt.text = "Hold E To Interact";

                if (Input.GetButtonDown("Interact"))
                {

                  holdTimer = hit.collider.gameObject.GetComponent<GameEventTrigger>().holdTimer;


                }
                if (Input.GetButton("Interact"))
                {
                    itemPrompt.text = "Working...";

                    holdTimer -= Time.deltaTime;

                    if (holdTimer < 0)
                    {
                        itemPrompt.text = "Done";

                        hit.collider.gameObject.GetComponent<GameEventTrigger>().TriggerEvent();


                    }

                }
                return;




            }

        }

        

        switch (currentHoldState)
        {
            case HoldState.notHoldingItem:
              
                break;
            case HoldState.holdingItem:
                itemPrompt.text = "";

                if (CanPutItemBack())
                {
                    currentHoldState = HoldState.putItemBack;
                }
                if (Input.GetButtonDown("Interact"))
                {
                    itemPrompt.text = "";
                    if (IsItemBehindWall())
                    {
                        return;
                    }
                    else
                    {
                        heldObject.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                        heldObject.gameObject.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * throwForce, ForceMode.Impulse);
                        PutDown();

                    }
                }
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
            case HoldState.putItemBack:
                if (!CanPutItemBack())
                {
                    currentHoldState = HoldState.holdingItem;
                }
                itemPrompt.text = "Put back " + heldItemData.itemname;

                if (Input.GetButton("Interact"))
                {

                    heldObject.transform.position = heldItemData.originalLocation;
                    heldObject.transform.rotation = heldItemData.originalRotation;
                    PutDown();
                }
                break;
        }
        

    }
}
*/
