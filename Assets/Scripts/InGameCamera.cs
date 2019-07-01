using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameCamera : MonoBehaviour
{
    public AudioSource spookyStingSource;
    public Image poloroidFrame;
    public float CameraRadius;
    public float CameraRange;
    public LayerMask clueLayerMask;
    public Texture2D photoTex;
    public GameObject photoTargetObject;
    public float photoDisplayTime;
    public Sprite emptySprite;
    public PhotoLibrary photoLibrary;
    public Slider energyBar;
    public Image energyFill;
    public GameObject spotLight;
    public int cameraShots;
    public float flashTime;
    public Canvas canvas;
    public GameObject blackoutPanel;
    public bool isFlashOn;
    public Ghost ghost;
    public float cameraTimer;
    public bool cameraIsActive =true;
    public float timeBetweenShots;
    public bool playerHasCamera;

    public void Start()
    {
        ghost = GameObject.Find("Ghost").GetComponent<Ghost>();

        spookyStingSource = GetComponent<AudioSource>();
        photoLibrary = GetComponent<PhotoLibrary>();
        cameraIsActive = false;
        isFlashOn = false;
        energyBar.maxValue = timeBetweenShots;
    }
    public void ActivateCamera()
    {

        if (playerHasCamera)
        {
            cameraIsActive = true;

        }

    }
    void Update()
    {

        if (cameraTimer < timeBetweenShots )
        {

            cameraTimer += Time.deltaTime;
        }
        
        energyBar.value = cameraTimer;
        if (cameraShots == 0)
        {
            energyFill.color = Color.red;
            cameraTimer = timeBetweenShots;
        }


        if (cameraIsActive)
        {
            if (Input.GetButton("AimCamera"))
            {

                if (Input.GetButtonDown("Interact"))
                {
                    if (cameraTimer > timeBetweenShots && cameraShots > 0)
                    {

                        }





    }

    IEnumerator TakePhoto()
    {
        ghost.ToggleGhost(true);
        yield return new WaitForSeconds(flashTime);
        ghost.ToggleGhost(false);
        

    }
}
