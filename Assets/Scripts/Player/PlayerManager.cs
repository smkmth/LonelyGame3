using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum PlayerState
{
    freeMovement,
    fullyPaused,
    cameraAimMode,
    menuMode,
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
    public MenuManager menuManager;

    public TextDisplayer textDisplayer;
    public GameObject PlayerCameraObject;
    public Camera playerCamera;
    public InGameCamera inGameCamera;
    public SmoothMouseLook mouseLook;
    private Inventory inv;

    public PlayerState currentPlayerState;
    public PlayerState nextPlayerState;

    public float playerStateTimer;
    public float playerStateChangeDelay;

    public float normalMoveSpeed;
    public float cameraMoveSpeed;
    public GameObject crosshair;

    public TextMeshProUGUI gameOverTextObj;
    public string gameWinText;
    public string gameLooseText;
    public TextMeshProUGUI hintText;
    public GameObject gameWinScreen;


    public bool showHints;
    public bool hintOnScreen;
    public float hintOnScreenTime;
    private float hintTimer;

    public List<string> seenHints;
    public bool viewingSeenHints;
    public TextMeshProUGUI seenHintsText;
    public GameObject seenHintsUI;
    public ObjectiveDisplayer objectiveDisplayer;
    public GameObject lampObject;

    public SaveLoad saveLoad;


    // Start is called before the first frame update
    void Start()
    {
        menuManager = GetComponent<MenuManager>();
        if (!menuManager)
        {

            Debug.LogError("No inventorydisplayer, add one to player obj" );
        }
        objectiveDisplayer = GetComponent<ObjectiveDisplayer>();

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

        ghostDetector = GetComponentInChildren<GhostDetector>();
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
        textDisplayer = GetComponent<TextDisplayer>();
        inv = GetComponent<Inventory>();
        saveLoad = GameObject.Find("GameReset").GetComponent<SaveLoad>();
        saveLoad.SaveGame();
       // Cursor.lockState = CursorLockMode.Locked;
    }

    public void GameLose()
    {

        ChangePlayerState(PlayerState.fullyPaused);
        gameWinScreen.SetActive(true);
        gameOverTextObj.text = gameLooseText;

    }

    public void WinGame()
    {
        ChangePlayerState(PlayerState.fullyPaused);
        gameWinScreen.SetActive(true);
        gameOverTextObj.text = gameWinText;
    }

    public void ChangePlayerState(PlayerState newPlayerState, bool stateChangeDelay =true)
    {
        Debug.Log("Change State from " + currentPlayerState + " To " + newPlayerState);
        if (newPlayerState == currentPlayerState)
        {
            return;
        }
        switch (newPlayerState)
        {
            case PlayerState.freeMovement:
                menuManager.ToggleMenu(false);
                Time.timeScale = 1;
                interact.interactIsActive = true;
                controller.characterIsActive = true;
                mouseLook.cameraControl = true;
                lamp.canUseLamp = true;
                inGameCamera.cameraIsActive = true;
                controller.playerCanRun = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                hintText.gameObject.SetActive(true);
                crosshair.SetActive(true);
                lampObject.SetActive(true);
                lamp.ToggleLamp(true);


                break;

           

            case PlayerState.fullyPaused:
                menuManager.ToggleMenu(false);
                interact.interactIsActive = false;
                controller.characterIsActive = false;
                controller.playerCanRun = false;
                mouseLook.cameraControl = false;
                inGameCamera.cameraIsActive =false;
                Cursor.visible = true;
                lamp.canUseLamp = true;
                hintText.gameObject.SetActive(false);
                lampObject.SetActive(true);

                Cursor.lockState = CursorLockMode.None;
                crosshair.SetActive(true);

                Time.timeScale = 0;
                break;
            case PlayerState.menuMode:
                interact.interactIsActive = false;
                controller.characterIsActive = false;
                controller.playerCanRun = false;
                mouseLook.cameraControl = false;
                inGameCamera.cameraIsActive = false;
                Cursor.visible = true;
                lamp.canUseLamp = true;
                hintText.gameObject.SetActive(false);
                lampObject.SetActive(false);

                Cursor.lockState = CursorLockMode.None;
                crosshair.SetActive(false);
                menuManager.ToggleMenu(true);

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
                lamp.canUseLamp = false;
                lamp.ToggleLamp(false);
                lampObject.SetActive(false);

                hintText.gameObject.SetActive(true);

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
        if (hintOnScreen)
        {
            hintTimer -= Time.deltaTime;
            if (hintTimer <= 0.0f)
            {
                hintText.text = "";
                hintOnScreen = false;
            }
        }
        switch (currentPlayerState)
        {
            case PlayerState.freeMovement:
                if (Input.GetButtonDown("ViewMenu"))
                {
                    ChangePlayerState(PlayerState.menuMode);
                }
                if (Input.GetButtonDown("ViewInv"))
                {
                    ChangePlayerState(PlayerState.menuMode);
                    menuManager.ToggleMenu(MenuType.Inventory);

                }
                if (Input.GetButtonDown("ViewObj"))
                {
                    ChangePlayerState(PlayerState.menuMode);
                    menuManager.ToggleMenu(MenuType.Objectives);

                }
                if (Input.GetButtonDown("ViewMap"))
                {
                    ChangePlayerState(PlayerState.menuMode);
                    menuManager.ToggleMenu(MenuType.Map);

                }
                if (Input.GetButtonDown("ViewReading"))
                {
                    ChangePlayerState(PlayerState.menuMode);
                    menuManager.ToggleMenu(MenuType.Reading);

                }
                break;
            case PlayerState.menuMode:
                if (Input.GetButtonDown("ViewMenu"))
                {
                    ChangePlayerState(PlayerState.freeMovement);
                }
                if (Input.GetButtonDown("ViewInv"))
                {
                    menuManager.ToggleMenu(MenuType.Inventory);
                }
                if (Input.GetButtonDown("ViewObj"))
                {
                    menuManager.ToggleMenu(MenuType.Objectives);
                }
                if (Input.GetButtonDown("ViewMap"))
                {
                    menuManager.ToggleMenu(MenuType.Map);
                }
                if (Input.GetButtonDown("ViewReading"))
                {
                    menuManager.ToggleMenu(MenuType.Reading);
                }
                break;
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
        if (Input.GetButtonDown("Help"))
        {
            if (viewingSeenHints)
            {

            }
        }
    }

    public void DisplayHint(string hintString ,float hintOnScreenTime)
    {
        if (showHints)
        {
            
            hintOnScreen = true;
            hintText.text += "\n Game Hint : " + hintString;
            seenHints.Add("\n " + hintString);
            hintTimer = hintOnScreenTime;

        }
        

    }
    public PlayerData SavePlayer()
    {
        PlayerData data = new PlayerData(inv.GetItemList(), objectiveDisplayer.GetObjectiveList(), textDisplayer.GetTextList());
        return data;

    }
    public void LoadPlayer(PlayerData data)
    {
        /*
        MasterAssetList masterAssetList = GameObject.Find("GameReset").GetComponent<MasterAssetList>();
        foreach(string item in data.playerItems)
        {
            if (item != null)
            {
                inv.AddItem(masterAssetList.findItemByName(item));
            }
        }
        foreach (string obj in data.playerObjectives)
        {
            objectiveDisplayer.AddObjective(masterAssetList.findObjectiveByName(obj));
        }
        foreach (string tex in data.playerTextAssets)
        {

            textDisplayer.AddTextAsset(masterAssetList.findTextByName(tex));
        }
        */

    }

    public void ToggleViewdHints()
    {
        ChangePlayerState(PlayerState.fullyPaused);


    }
}

[System.Serializable]
public class PlayerData
{
    public string[]      playerItems;
    public string[]     playerObjectives;
    public string[]    playerTextAssets;
    public Vector3 playerPos;
    public PlayerData(string[] _playerItems, string[] _playerObjectives, string[] _playerTextAssets)
    {
        this.playerItems = _playerItems;
        this.playerObjectives = _playerObjectives;
        this.playerTextAssets = _playerTextAssets;
    }
}

