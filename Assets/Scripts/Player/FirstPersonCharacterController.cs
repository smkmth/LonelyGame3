using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum FloorType
{
    Carpet,
    Stone,
    Wood

}
public class FirstPersonCharacterController : MonoBehaviour
{

    [Header("Player Audio")]
    public AudioSource audioSource;
    public AudioClip[] woodFootsteps;
    public AudioClip[] stoneFootsteps;
    public AudioClip[] carpetFootsteps;
    public FloorType currentFloorType;
    public int helperId = 39;
    public float footstepTime;
    public float footstepRunTime;
    private float footsteptimer =0.0f;


    Rigidbody rb;
    public float moveSpeed;
    public GameObject playerCamera;
    public float maxSpeed;
    public bool moving;
    public float runSpeed;
    public float maxRunSpeed;
    public bool isRunning;
    public bool characterIsActive= true;
    public float sprintTime;

    public float sprintTimer;
    public bool enoughStamina;

    public bool playerCanRun;
    public Transform standingPos;
    public Vector3 crouchingScale;
    public Transform crouchingPos;
    public bool playerIsCrouching;
    public Transform playerCollider;
    public LayerMask groundLayer;

    public float heightdist;
    public float fallmod;

    public Vector3 sprintMax;
    public Vector3 sprintMin;
    public bool coroutineRunning;
    Coroutine sprintDown;
    Coroutine sprintUp;
    public float playerHeight;
    public float playerLowHeight;
    public float playerCrouchedHeight;
    public float playerCrouchedLowHeight;
    public Vector3 playerStart;
    public float upAmount;
    public Transform sprintVingetteObj;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerStart = transform.position;
        sprintVingetteObj.localPosition = new Vector3(0, sprintTime, 0);
    }


    public void ToggleCrouch(bool isCrouching)
    {
        float crouchDist = (crouchingScale.y - 1.0f);
        if (isCrouching)
        {

            playerIsCrouching = true;
            playerCollider.localScale= crouchingScale;
;
        }
        else
        {
           
            if (CanStand())
            {

                playerIsCrouching = false;
                playerCollider.localScale = Vector3.one;
            }

        }

    }
    public bool CanStand()
    {
        if(Physics.Raycast(transform.position,Vector3.up, 1))
        {
            Debug.Log("somthing up");
            return false;
        }
        else
        {
            return true;

        }
    }
    public void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, groundLayer))
        {
            if (hit.collider.tag == "Carpet")
            {
                currentFloorType = FloorType.Carpet;

            }
            if (hit.collider.tag == "Wood")
            {
                currentFloorType = FloorType.Wood;

            }
            if (hit.collider.tag == "Stone")
            {
                currentFloorType = FloorType.Stone;

            }
            heightdist = transform.position.y - hit.point.y;
            Debug.DrawRay(transform.position, Vector3.down * Vector3.Distance(transform.position, hit.point * heightdist));
        }

        if (Input.GetButtonDown("FixMe"))
        {
            transform.position = playerStart; 
        }
     
        if (Input.GetButton("GoUp"))
        {
            Debug.Log("tryingup");
            transform.position += Vector3.up * upAmount;

        }
        if (Input.GetButtonDown("VolUp"))
        {
            AudioListener.volume += 3.0f;
        }
        if (Input.GetButtonDown("VolDown"))
        {
            AudioListener.volume -= 3.0f;
        }

     

        if (characterIsActive)
        {
            if (Input.GetButton("Crouch"))
            {
                if (!playerIsCrouching)
                {
                    ToggleCrouch(true);

                }
            }
            else
            {
                if (playerIsCrouching)
                {
                    ToggleCrouch(false);
                }
            }
            if (playerCanRun)
            {
                
                
                if (Input.GetButton("Run"))
                {
                    if (enoughStamina)
                    {
                        isRunning = true;
                    }
                }
                else
                {

                    
                    isRunning = false;

                }
            }
   
            if (isRunning)
            {
                if (sprintTimer < sprintTime)
                {


                    sprintTimer += Time.deltaTime;
                }
                else
                {
                    enoughStamina = false;
                    isRunning = false;

                }


            }
            else
            {
                if (sprintTimer > 0)
                {

                    sprintTimer -= Time.deltaTime;
                }
                else
                {
                    enoughStamina = true;
                }


            }
     
            if (moving)
            {
                if (!isRunning)
                {

                    if (footsteptimer >= footstepTime)
                    {
                        if (currentFloorType == FloorType.Wood)
                        {

                            HelperFunctions.Helper.PlayRandomNoiseInArray(woodFootsteps,audioSource, 1,helperId);
                        }
                        else if (currentFloorType == FloorType.Carpet)
                        {
                            HelperFunctions.Helper.PlayRandomNoiseInArray(carpetFootsteps, audioSource, 1, helperId);

                        }
                        else if (currentFloorType == FloorType.Stone)
                        {
                            HelperFunctions.Helper.PlayRandomNoiseInArray(stoneFootsteps, audioSource, 1, helperId);

                        }
                        else
                        {
                            HelperFunctions.Helper.PlayRandomNoiseInArray(woodFootsteps, audioSource, 1, helperId);

                        }
                        footsteptimer = 0;
                    }
                    else
                    {
                        footsteptimer += Time.deltaTime;
                    }
                }
                else
                {

                    if (footsteptimer >= footstepRunTime)
                    {
                        if (currentFloorType == FloorType.Wood)
                        {

                            HelperFunctions.Helper.PlayRandomNoiseInArray(woodFootsteps, audioSource, 1, helperId);
                        }
                        else if (currentFloorType == FloorType.Carpet)
                        {
                            HelperFunctions.Helper.PlayRandomNoiseInArray(carpetFootsteps, audioSource, 1, helperId);

                        }
                        else if (currentFloorType == FloorType.Stone)
                        {
                            HelperFunctions.Helper.PlayRandomNoiseInArray(stoneFootsteps, audioSource, 1, helperId);

                        }
                        else
                        {
                            HelperFunctions.Helper.PlayRandomNoiseInArray(woodFootsteps, audioSource, 1, helperId);

                        }
                        footsteptimer = 0;
                    }
                    else
                    {
                        footsteptimer += Time.deltaTime;
                    }
                }

            }
        }

        sprintVingetteObj.localPosition = new Vector3(0, (sprintTime - sprintTimer), 0);


    }

    private void FixedUpdate()
    {
        if (!playerIsCrouching)
        {
            if (heightdist > playerHeight)
            {
                rb.AddForce(Vector3.down * fallmod, ForceMode.Acceleration);
            }
            else if (heightdist < playerLowHeight)
            {

                rb.AddForce(Vector3.up * fallmod, ForceMode.Acceleration);
            }
        }
        else
        {
            if (heightdist > playerCrouchedHeight)
            {

                rb.AddForce(Vector3.down * fallmod, ForceMode.Acceleration);
            }
            else if (heightdist < playerCrouchedLowHeight)
            {

                rb.AddForce(Vector3.up * fallmod, ForceMode.Acceleration);
            }

        }
        if (characterIsActive)
        {



            float inputX = Input.GetAxis("Horizontal");
            float inputY = Input.GetAxis("Vertical");
            Vector3 movementVector = new Vector3(inputX, 0, inputY);
            if (movementVector != Vector3.zero)
            {
                moving = true;
            }
            else
            {
                moving = false;

            }
         

            movementVector = playerCamera.transform.TransformDirection(movementVector);
            movementVector.y = 0;
            if (!isRunning)
            {

                if (rb.velocity.magnitude < maxSpeed)
                {

                    rb.AddForce(movementVector * moveSpeed, ForceMode.Acceleration);



                }
            }
            else
            {
                if (rb.velocity.magnitude < maxRunSpeed)
                {

                    rb.AddForce(movementVector * runSpeed, ForceMode.Acceleration);

                }

            }
        }


    }

}