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
    public TextMeshProUGUI cameraCounter;
    public PlayerManager manager;
    private bool startedAiming;
    public void Start()
    {
        ghost = GameObject.Find("Ghost").GetComponent<Ghost>();

        spookyStingSource = GetComponent<AudioSource>();
        photoLibrary = GetComponent<PhotoLibrary>();
        cameraIsActive = false;
        isFlashOn = false;
        energyBar.maxValue = timeBetweenShots;
    }

    void Update()
    {
        if (!playerHasCamera)
        {
            return;
        }

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

        if (!cameraIsActive)
        {
            manager.ChangePlayerState(PlayerState.freeMovement);
            return;
        }
        


        if (Input.GetButton("AimCamera"))
        {
            if (!startedAiming){
                manager.ChangePlayerState(PlayerState.cameraAimMode);
                startedAiming = true;

            }



            if (Input.GetButtonDown("Interact"))
            {
                if (cameraTimer > timeBetweenShots && cameraShots >= 0)
                {
                    cameraShots--;
                    cameraCounter.text = cameraShots.ToString();
                    cameraTimer = 0;
                    StartCoroutine(TakePhoto());
                }

            }
        }
        else
        {
            manager.ChangePlayerState(PlayerState.freeMovement);
            startedAiming = false;

        }








    }

    IEnumerator TakePhoto()
    {
        ghost.ToggleGhost(true);
        yield return new WaitForSeconds(flashTime);
        ghost.ToggleGhost(false);
        

    }
    public void UpdateShots()
    {
        cameraCounter.text = cameraShots.ToString();

    }
}
