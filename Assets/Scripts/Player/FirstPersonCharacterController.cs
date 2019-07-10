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
    public Slider energyBar;

    public GameObject tutorial;
    public bool tutorialOn;
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

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        audioSource = GetComponent<AudioSource>();
        energyBar.maxValue = sprintTime;

    }

    public void ToggleHelp()
    {
        if (!tutorialOn)
        {
            tutorialOn = true;
            Time.timeScale = 0.0f;
            tutorial.SetActive(true);
        }
        else
        {
            tutorialOn = false;
            Time.timeScale = 1.0f;
            tutorial.SetActive(false);
        }

    }
    public void ToggleCrouch(bool isCrouching)
    {
        if (isCrouching)
        {
            playerCamera.transform.position = crouchingPos.position;
            playerCollider.localScale= crouchingScale;
            playerIsCrouching = true;
            transform.position = new Vector3(transform.position.x, .3f, transform.position.z);
        }
        else
        {
           
            if (CanStand())
            {

                transform.position = new Vector3(transform.position.x, .8f, transform.position.z);
                playerCollider.localScale = Vector3.one;
                playerIsCrouching = false;
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
            heightdist = transform.position.y - hit.point.y;
            Debug.DrawRay(transform.position, Vector3.down * Vector3.Distance(transform.position, hit.point * heightdist));
        }
        if (heightdist > 1f)
        {
            rb.AddForce(Vector3.down *  fallmod, ForceMode.Acceleration);
        }
        if (Input.GetButtonDown("VolUp"))
        {
            AudioListener.volume += 3.0f;
        }
        if (Input.GetButtonDown("VolDown"))
        {
            AudioListener.volume -= 3.0f;
        }

        if (Input.GetButtonDown("Help"))
        {
            ToggleHelp();
        }

        energyBar.value = sprintTimer;
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
    void StartLerpingPos()
    {
        isLerping = true;
        timeStartedLerp = Time.time;
    }
    private void FixedUpdate()
    {
  
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
/*
    IEnumerator Vingette(Vector3 targetScale, float duration)
    {

        Vector3 startScale = sprintVingette.transform.localScale;
        float time = 0;

        while (time < duration)
        {
            coroutineRunning = true;
            time += Time.deltaTime;
            float blend = Mathf.Clamp(time, 0, duration);
            sprintVingette.transform.localScale = Vector3.Lerp(startScale, targetScale, blend);
            yield return new WaitForEndOfFrame();
        }

        Debug.Log("here");
        coroutineRunning = false;
    }
    */
}