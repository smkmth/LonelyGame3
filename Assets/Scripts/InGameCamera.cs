using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public bool isCameraActive;
    private SpriteRenderer currentGhost;
    public GameObject spotLight;
    public int cameraShots;
    public float flashTime;
    public Canvas canvas;
    public GameObject blackoutPanel;
    public bool isFlashOn;
    public Ghost ghost;
    public float cameraTimer;
    public bool cameraIsActive;
    public float timeBetweenShots;

    public void Start()
    {
        ghost = GameObject.Find("Ghost").GetComponent<Ghost>();

        spookyStingSource = GetComponent<AudioSource>();
        photoLibrary = GetComponent<PhotoLibrary>();
        isCameraActive = true;
        isFlashOn = false;
        energyBar.maxValue = timeBetweenShots;
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

  
        if (Input.GetButtonDown("Flash"))
        {
            if (isFlashOn)
            {
                isFlashOn = false;
            }
            else
            {
                isFlashOn = true;
            }
        }

        if (cameraIsActive)
        {
            if (Input.GetButtonDown("TakePhoto"))
            {
                if (cameraTimer > timeBetweenShots && cameraShots > 0)
                {
                    cameraTimer = 0;
                    RaycastHit[] targets = Physics.SphereCastAll(transform.position, CameraRadius, transform.forward, CameraRange, clueLayerMask);
                    foreach (RaycastHit hit in targets)
                    {
                        if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Clue"))
                        {
                            Clue clue = hit.collider.gameObject.GetComponent<Clue>();
                            if (clue.clueType != ClueType.PhotoTarget)
                            {
                                return;
                            }
                            if (!clue.foundClue)
                            {
                                clue.foundClue = true;
                                energyBar.value += clue.energyValue;
                            }
                        }
                        else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ghost"))
                        {
                            if (energyBar.value == 100)
                            {

                                currentGhost = hit.collider.gameObject.GetComponent<SpriteRenderer>();
                                currentGhost.enabled = true;
                                spookyStingSource.Play();
                            }
                        }
                    }
                    cameraShots--;
                    StartCoroutine(TakePhoto());
                }

            }
        }

        if (Input.GetButtonDown("ViewPhotos"))
        {
            photoLibrary.TogglePhotoLibrary();
            if (photoLibrary.isLibraryActive)
            {
                isCameraActive = false;
            }
            else
            {
                isCameraActive = true;

            }
        }


    }

    IEnumerator TakePhoto()
    {
        ghost.ToggleGhost(true);
        yield return new WaitForSeconds(flashTime);
        ghost.ToggleGhost(false);

    }
}
