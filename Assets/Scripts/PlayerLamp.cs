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
            playerLight.ToggleLight(lampOn);
            StartLerpingPos();
            if (lampOn)
            {
                lampOn = false;
            }
            else
            {
                lampOn = true;
            }

        }
    }

    void StartLerpingPos()
    {
        isLerping = true;
        timeStartedLerp = Time.time;


    }

}
