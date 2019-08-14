using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLamp : MonoBehaviour
{
    public CandleFlicker playerLight;
    public Transform playerLightPos;
    public bool lampOn;

    public Transform heldPos;
    public Transform droppedPos;
    public float timeToDrop;

    private float timeStartedLerp;
    private bool isLerping;
    public bool canUseLamp;
    public GameObject fillLight;
    public AudioSource source;
    public AudioClip gasLampLight;
    public AudioClip gasLampLit;
    // Start is called before the first frame update
    void Start()
    {
        lampOn =true;

    }

    // Update is called once per frame
    void Update()
    {
        if (isLerping)
        {
            float timeSinceStarted = Time.time - timeStartedLerp;
            float percentageComplete = timeSinceStarted / timeToDrop;
            if (!lampOn)
            {

                playerLightPos.position = Vector3.Lerp(heldPos.position,droppedPos.position, percentageComplete);
            }
            else
            {

                playerLightPos.position = Vector3.Lerp(droppedPos.position,  heldPos.position, percentageComplete);
            }


            if (percentageComplete >= 1.0f)
            {
                isLerping = false;
            }

        }
        if (Input.GetButtonDown("Lamp"))
        {
            if (canUseLamp)
            {
                if (lampOn)
                {
                    source.Stop();
                    lampOn = false;
                    ToggleLamp(false);
                    fillLight.SetActive(false);
                }
                else
                {
                    source.PlayOneShot(gasLampLight ,1);
                    source.clip = gasLampLit;
                    source.volume = .01f;
                    source.Play();
                    source.loop = true;
                    lampOn = true;
                    ToggleLamp(true);
                    fillLight.SetActive(true);
                }

            }
           

        }
    }

    void StartLerpingPos()
    {
        isLerping = true;
        timeStartedLerp = Time.time;


    }
    public void ToggleLamp(bool isLampOn )
    {
        lampOn = isLampOn;
        playerLight.ToggleLight(isLampOn);
        StartLerpingPos();
      
    }

}
