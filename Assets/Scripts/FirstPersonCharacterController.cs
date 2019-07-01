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
    public bool canRun;
    public Slider energyBar;

    public GameObject tutorial;
    public bool tutorialOn;

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
    public void Update()
    {
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


            if (Input.GetButton("Run"))
            {
                if (canRun)
                {
                    isRunning = true;
                }
            }
            else
            {
                isRunning = false;

            }


            if (isRunning)
            {
                if (sprintTimer < sprintTime)
                {
                    sprintTimer += Time.deltaTime;
                }
                else
                {
                    canRun = false;
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
                    canRun = true;
                }


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