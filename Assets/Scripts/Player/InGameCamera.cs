using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameCamera : MonoBehaviour
{
    
    private GameObject playerObj;                               //Player Obj got by name "Player"
    private PlayerManager manager;                              //PlayerManager, used for switching player states between camera aim and normal state

    [Header("Camera Values")]

    public int maxShots= 12;                             //the max amount of shots that you can carry
   public int cameraShots;                          //
    public float timeBetweenShots;
   public float timerBetweenShotsTime;
    public float shutterClosedTime;
   public bool reloadingFilm;
    public int filmCanisters;
    public float reloadTime;
   public float reloadTimer;

    [Header("Camera Ghost Interaction")]
    public float CameraRadius;
    public float CameraRange;
    public LayerMask ghostLayerMask;
    public LayerMask normalLayerMask;
    public float ghostOnScreenTime;
    public Ghost ghostObj;

    public bool playerHasCamera =false;

   public bool cameraIsActive =true;
   public bool justStartedAiming;
   public bool justStoppedAiming;

    [Header("Camera Effects")]
    public GameObject cameraOverlay;
    public GameObject shutterObj;
    public Transform shutterDefaultPos;
    private Vector3 shutterStartPos;
    private Vector3 shutterEndPos;

    public Slider energyBar;
    public Image energyFill;

    [Header("Camera Audio")]
    public AudioSource source;
    public AudioClip cameraReady;
    public AudioClip cameraShot;

    [Header("Film Counter")]
    public Image filmCounterRect;
    public Vector3 filmCounterStartPos;
    public Vector3 filmCounterEndPos;
    public Vector3 filmCounterCurrentPos;
    public float cameraOffSet;           //offset is how far down the camera reel should move after a shot,
    public TextMeshProUGUI shotCountText;                                                  //start pos of camera is -254, end is 483, 737 discrete measurements to move through
                                                                                           // 737/32 = 23 units to move down each time.
    public float blinkTime;
    float blinkTimer;
    public GameObject readyObj;
    public GameObject emptyObj;

    public void Start()
    {
        playerObj = GameObject.Find("Player");
        manager = playerObj.GetComponent<PlayerManager>();
        ghostObj = GameObject.Find("Ghost").GetComponent<Ghost>();
        cameraIsActive = false;
        filmCounterStartPos = filmCounterRect.rectTransform.localPosition;
        filmCounterEndPos = filmCounterStartPos;
        filmCounterEndPos.x -= cameraOffSet *maxShots;
        cameraShots = maxShots;
        reloadTimer = reloadTime;
    }

    void Update()
    {
        
        if (!playerHasCamera)
        {
            return;
        }
        if (reloadingFilm)
        {
            emptyObj.SetActive(false);
            if (reloadTimer >= 0)
            {
                blinkTimer -= Time.deltaTime;
                if (blinkTimer < 0)
                {
                    blinkTimer = blinkTime;
                    readyObj.SetActive(!readyObj.activeSelf);
                }
             
                // filmCounterRect.rectTransform.localPosition = Vector3.Lerp( filmCounterStartPos, filmCounterRect.rectTransform.localPosition, Mathf.Clamp01((reloadTimer / reloadTime)));
                filmCounterRect.rectTransform.localPosition = Vector3.Lerp( filmCounterStartPos, filmCounterEndPos, Mathf.Clamp01((reloadTimer / reloadTime)));
                reloadTimer -= Time.deltaTime;
            }
            else
            {

                readyObj.SetActive(true);
                filmCounterRect.rectTransform.localPosition = filmCounterStartPos;
                cameraShots = maxShots;
                reloadingFilm = false;
                reloadTimer = reloadTime;
                shotCountText.text = cameraShots.ToString();
            }
        }
        if (timerBetweenShotsTime < timeBetweenShots )
        {
            readyObj.SetActive(false);
            timerBetweenShotsTime += Time.deltaTime;
        }
        else if ((timerBetweenShotsTime > timeBetweenShots) && !reloadingFilm && cameraShots > 0)
        {
            readyObj.SetActive(true);


        }


        if (cameraShots == 0 && !reloadingFilm)
        {
            blinkTimer -= Time.deltaTime;
            if (blinkTimer < 0)
            {
                blinkTimer = blinkTime;
                emptyObj.SetActive(!emptyObj.activeSelf);
            }
        }


        if (!cameraIsActive)
        {
            return;
        }
        
        if (Input.GetButton("ReloadCamera"))
        {
            if ( filmCanisters > 0 )
            {
                if (cameraShots == 0)
                {

                    filmCounterCurrentPos = filmCounterRect.rectTransform.localPosition;
                    filmCanisters -= 1;
                    reloadingFilm = true;
                }
            }

        }

        if (Input.GetButton("AimCamera"))
        {
            if (!justStartedAiming)
            {
                ToggleAiming(true);
            }
            shutterObj.transform.position = shutterDefaultPos.position;
            
            if (Input.GetButtonDown("Interact"))
            {
                if (!reloadingFilm)
                {

                    if (timerBetweenShotsTime > timeBetweenShots && cameraShots >= 1)
                    {
                        timerBetweenShotsTime = 0;
                        StartCoroutine(TakePhoto());
                        UpdateShots(-1);
                    }
                }

            }
        }
        else
        {
            if (!justStoppedAiming)
            {
                ToggleAiming(false);
  
            }
        }

    }
    public void ToggleAiming(bool isAiming)
    {
        if (isAiming)
        {

            manager.ChangePlayerState(PlayerState.cameraAimMode);
            justStartedAiming = true;
            justStoppedAiming = false;
            cameraOverlay.SetActive(true);
            source.PlayOneShot(cameraReady, 1.0f);
        }
        else
        {
            justStartedAiming = false;
            justStoppedAiming = true;
            manager.ChangePlayerState(PlayerState.freeMovement);
            cameraOverlay.SetActive(false);
        }

    }
    IEnumerator TakePhoto()
    {
        shutterObj.SetActive(true);
        
        float elapsedTime =0;
        shutterStartPos = shutterObj.transform.position;
        shutterEndPos = shutterObj.transform.position - new Vector3(0, 0.9f, 0);
        while (elapsedTime < shutterClosedTime)
        {
            shutterObj.transform.position = Vector3.Lerp(shutterStartPos, shutterEndPos, (elapsedTime / shutterClosedTime));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            

        }
        source.PlayOneShot(cameraShot, 3.0f);
        elapsedTime = 0;
        while (elapsedTime < shutterClosedTime)
        {
            shutterObj.transform.position = Vector3.Lerp(shutterEndPos, shutterStartPos , (elapsedTime / shutterClosedTime));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();


        }

        shutterObj.SetActive(false);

        ghostObj.ToggleGhost(true);
        Camera.main.cullingMask = ghostLayerMask;
        yield return new WaitForSeconds(ghostOnScreenTime);
        ghostObj.ToggleGhost(false);
        Camera.main.cullingMask = normalLayerMask;


    }
    public void UpdateShots(int shotsToAdd)
    {
        //check if this new film will overload the camera.
        if ((cameraShots + shotsToAdd) <= maxShots)
        {
            cameraShots += shotsToAdd;
            float thisOffset = shotsToAdd * cameraOffSet;
            shotCountText.text = cameraShots.ToString();
            Vector3 shotoffset = new Vector3(thisOffset, 0, 0);
            filmCounterRect.rectTransform.localPosition = filmCounterRect.rectTransform.localPosition + shotoffset;
        }
       
    }

    public IEnumerator ReloadFilm()
    {
        float elapsedTime = 0;
        

        while (elapsedTime <= reloadTime)
        {
            filmCounterRect.rectTransform.localPosition = Vector3.Lerp(filmCounterStartPos, filmCounterEndPos, Mathf.Clamp01((elapsedTime / reloadTime)));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();


        }

        yield return new WaitForEndOfFrame();
    }
}
