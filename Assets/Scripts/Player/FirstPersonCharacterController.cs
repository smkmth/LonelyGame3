using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstPersonCharacterController : MonoBehaviour
{
    public AudioClip[] footsteps;
    private AudioSource audioSource;
    Rigidbody rb;
    public float moveSpeed;
    public GameObject playerCamera;
    public float maxSpeed;
    public bool moving;
    public float runSpeed;
    public float maxRunSpeed;
    public bool isRunning;
    private float footsteptimer =0.0f;
    public float footstepTime;
    public float footstepRunTime;
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
    bool isLerping;
    private float timeStartedLerp;
    public Image sprintVingette;
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

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        audioSource = GetComponent<AudioSource>();
        playerStart = transform.position;
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
        if(Physics.Raycast(transform.position, Vector3.down, out hit,groundLayer))
        {
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
            int index = Random.Range(0, footsteps.Length);
            AudioClip footstep = footsteps[index];
            if (moving)
            {
                if (!isRunning)
                {

                    if (footsteptimer >= footstepTime)
                    {
                        audioSource.clip = footstep;
                        audioSource.PlayOneShot(footstep, 0.5f);
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
                        audioSource.clip = footstep;
                        audioSource.PlayOneShot(footstep, 0.5f);
                        footsteptimer = 0;
                    }
                    else
                    {
                        footsteptimer += Time.deltaTime;
                    }
                }

            }
        }

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