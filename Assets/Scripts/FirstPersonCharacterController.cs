﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCharacterController : MonoBehaviour
{
    public AudioClip[] footsteps;
    private AudioSource audioSource;
    Rigidbody rb;
    public float moveSpeed;
    public GameObject playerCamera;
    public float maxSpeed;
    public bool walking;
    public float runSpeed;
    public float maxRunSpeed;
    public bool isRunning;
    private float footsteptimer =0.0f;
    public float footstepTime;
    public float footstepRunTime;
    public bool characterIsActive= true;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        audioSource = GetComponent<AudioSource>();
    }


    public void Update()
    {
        if (characterIsActive)
        {

            int index = Random.Range(0, footsteps.Length);
            AudioClip footstep = footsteps[index];
            if (walking)
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
        if (Input.GetButton("Run"))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;

        }
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        Vector3 movementVector = new Vector3(inputX, 0, inputY);
        if (movementVector != Vector3.zero)
        {
            walking = true;
        }
        else
        {
            walking = false;

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