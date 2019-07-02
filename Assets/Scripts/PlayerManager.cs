using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerState
{
    freeMovement,
    noMovement,
    noCameraMovement,
    inspectMode,
    noCameraAndMovementCursorFree,
    fullyPaused,
    cameraAimMode,
    delay
}
public class PlayerManager : MonoBehaviour
{

    public FirstPersonCharacterController controller;
    public Rigidbody rb;
    public AudioSource footstepsAudioSource;
    public GhostDetector ghostDetector;
    public InGameTextReader reader;
    public PlayerInteract interact;
    public PlayerLamp lamp;

    public GameObject PlayerCameraObject;
    public Camera playerCamera;
    public InGameCamera inGameCamera;
    public SmoothMouseLook mouseLook;

    public PlayerState currentPlayerState;
    public PlayerState nextPlayerState;

    public float playerStateTimer;
    public float playerStateChangeDelay;

    public float normalMoveSpeed;
    public float cameraMoveSpeed;
    public GameObject crosshair;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<FirstPersonCharacterController>();
        if (!controller)
        {
            Debug.LogError("No first person character controller, add one to player obj" );
        }
        rb = GetComponent<Rigidbody>();
        if (!rb)
        {
            Debug.LogError("No Rigidbody, add one to player obj");
        }
        footstepsAudioSource = GetComponent<AudioSource>();
        if (!footstepsAudioSource)
        {
            Debug.LogError("No audiosource, add one to player obj");
        }

        ghostDetector = GetComponent<GhostDetector>();
        if (!ghostDetector)
        {
            Debug.LogError("No ghostdetector, add one to player obj");

        }
        reader = GetComponent<InGameTextReader>();
        if (!reader)
        {
            Debug.LogError("No reader, add one to player obj");

        }
        interact = GetComponent<PlayerInteract>();
        if (!interact)
        {
            Debug.LogError("No playerinteract, add one to player obj");

        }
        lamp = GetComponent<PlayerLamp>();
        if (!lamp)
        {
            Debug.LogError("No playerlamp, add one to player obj");

        }
        playerCamera = PlayerCameraObject.GetComponent<Camera>();
        if (!playerCamera)
        {
            Debug.LogError("No camera , add one to player camera obj");

        }
        inGameCamera = PlayerCameraObject.GetComponent<InGameCamera>();
        if (!inGameCamera)
        {
            Debug.LogError("No ingameCamera object , add one to player camera obj");

        }
        mouseLook = PlayerCameraObject.GetComponent<SmoothMouseLook>();
        if (!mouseLook)
        {
            Debug.LogError("No mouselook , add one to player camera obj");

        }
    }

    public void ChangePlayerState(PlayerState newPlayerState, bool stateChangeDelay =true)
    {
        if (newPlayerState == currentPlayerState)
        {
            return;
        }
        switch (newPlayerState)
        {
            case PlayerState.freeMovement:
                Time.timeScale = 1;
                interact.interactIsActive = true;
                controller.characterIsActive = true;
                mouseLook.cameraControl = true;

                inGameCamera.cameraIsActive = true;
                controller.playerCanRun = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                crosshair.SetActive(true);

                break;

            case PlayerState.noMovement:
                interact.interactIsActive = true;
                Time.timeScale = 1;
                controller.characterIsActive = false;
                mouseLook.cameraControl = true;

                inGameCamera.cameraIsActive = false;
                controller.playerCanRun = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                crosshair.SetActive(true);

                break;
            case PlayerState.noCameraMovement:
                interact.interactIsActive = true;
                Time.timeScale = 1;
                controller.characterIsActive = true;
                mouseLook.cameraControl = false;
                inGameCamera.cameraIsActive = false;
                controller.playerCanRun = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                crosshair.SetActive(true);


                break;
            case PlayerState.inspectMode:
                interact.interactIsActive = true;
                Time.timeScale = 1;
                controller.characterIsActive = false;
                controller.playerCanRun = false;

                mouseLook.cameraControl = false;
                inGameCamera.cameraIsActive = false;

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                crosshair.SetActive(true);

                break;
            case PlayerState.noCameraAndMovementCursorFree:
                Time.timeScale = 1;
                interact.interactIsActive = false;
                controller.playerCanRun = false;
                controller.characterIsActive = false;
                mouseLook.cameraControl = false;
                inGameCamera.cameraIsActive = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                crosshair.SetActive(true);

                break;

            case PlayerState.fullyPaused:
                interact.interactIsActive = false;
                controller.characterIsActive = false;
                controller.playerCanRun = false;
                mouseLook.cameraControl = false;
                inGameCamera.cameraIsActive =false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                crosshair.SetActive(true);

                Time.timeScale = 0;
                break;
            case PlayerState.cameraAimMode:
                Time.timeScale = 1;
                interact.interactIsActive = true;
                controller.characterIsActive = true;
                controller.playerCanRun = false;
                mouseLook.cameraControl = true;
                inGameCamera.cameraIsActive = true;
                crosshair.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;


            case PlayerState.delay:
                break;
        }
        if (stateChangeDelay)
        {
            nextPlayerState = newPlayerState;
            currentPlayerState = PlayerState.delay;

        }
        else
        {
            currentPlayerState = newPlayerState;

        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentPlayerState)
        {
            case PlayerState.delay:
                if (playerStateTimer<= 0)
                {
                    currentPlayerState = nextPlayerState; 
                }
                else
                {
                    playerStateChangeDelay -= Time.unscaledDeltaTime;
                }
                break;
        }
    }
}
