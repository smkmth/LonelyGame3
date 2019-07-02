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
    public GameObject cameraLense;
    public GameObject shutter;
    public float shutterTime;
    public Vector3 shutterStartPos;
    public Vector3 shutterEndPos;
    public Transform shutterDefaultPos;

    public float cameraOffSet =17;
    public TextMeshProUGUI filmCounterRect;

    public AudioSource source;
    public AudioClip cameraReady;
    public AudioClip cameraShot;

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
                cameraLense.SetActive(true);
                source.PlayOneShot(cameraReady, 1.0f);

            }
            shutter.transform.position = shutterDefaultPos.position;



            if (Input.GetButtonDown("Interact"))
            {
                if (cameraTimer > timeBetweenShots && cameraShots >= 0)
                {
                    cameraCounter.text = cameraShots.ToString();
                    cameraTimer = 0;
                    StartCoroutine(TakePhoto());
                    UpdateShots(-1);
                }

            }
        }
        else
        {
            manager.ChangePlayerState(PlayerState.freeMovement);
            cameraLense.SetActive(false);
            startedAiming = false;

        }








    }

    IEnumerator TakePhoto()
    {
        shutter.SetActive(true);
        
        float elapsedTime =0;
        shutterStartPos = shutter.transform.position;
        shutterEndPos = shutter.transform.position - new Vector3(0, 0.9f, 0);

        while (elapsedTime < shutterTime)
        {
            shutter.transform.position = Vector3.Lerp(shutterStartPos, shutterEndPos, (elapsedTime / shutterTime));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            

        }
        source.PlayOneShot(cameraShot, 3.0f);
        elapsedTime = 0;
        while (elapsedTime < shutterTime)
        {
            shutter.transform.position = Vector3.Lerp(shutterEndPos, shutterStartPos , (elapsedTime / shutterTime));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();


        }

        shutter.SetActive(false);

        ghost.ToggleGhost(true);
        yield return new WaitForSeconds(flashTime);
        ghost.ToggleGhost(false);
        

    }
    public void UpdateShots(int shotsToAdd)
    {
        cameraShots += shotsToAdd;
        float thisOffset = shotsToAdd * cameraOffSet;
        Vector3 shotoffset = new Vector3(0, thisOffset, 0);
        filmCounterRect.rectTransform.localPosition = filmCounterRect.rectTransform.localPosition + shotoffset;
        cameraCounter.text = cameraShots.ToString();

    }
}
