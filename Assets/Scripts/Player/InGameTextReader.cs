using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InGameTextReader : MonoBehaviour
{
    public GameObject ReaderUI;
    public TextMeshProUGUI displayText;
    public TextMeshProUGUI titleText;
    public ScrollRect textRect;
    public FirstPersonCharacterController controller;
    public SmoothMouseLook mouseLook;
    public Button returnButton;
    public InGameCamera gameCamera;
    public TextDisplayer textDisplayer;

    public PlayerManager playerManager;
    public MenuManager menuManager;

    private float timer;

    public float inputInactive =.3f;

    private bool canClick = false;
    public bool playerIsReading =false;

    public void Start()
    {
        menuManager = GetComponent<MenuManager>();
        playerManager = GetComponent<PlayerManager>();
        textDisplayer = GetComponent<TextDisplayer>();
        returnButton.onClick.AddListener(() => ResumeGame());
        ReaderUI.SetActive(false);
    }

    private void Update()
    {
        if (!playerIsReading)
        {
            return;
        }

        if (timer <= 0)
        {
            canClick = true;
        }
        else
        {
            timer -= Time.unscaledDeltaTime;
        }
        /*


        if (canClick)
        {
           

            if (Input.GetButtonDown("Interact"))
            {
                ResumeGame();
            }
        }
        */
      
    }

    public void ScrollDebug(Vector2 scroll)
    {
        Debug.Log(scroll);
    }

    public void ReturnToMenu()
    {

        playerIsReading = false;
        ReaderUI.SetActive(false);
        displayText.text = "";
        titleText.text = "";
        playerManager.ChangePlayerState(PlayerState.menuMode);


    }
    public void DisplayText(InGameText text)
    {
        //textRect.normalizedPosition = new Vector2(textRect.normalizedPosition.x, 1);
        playerIsReading = true;
        canClick = false;
        timer = inputInactive;
        ReaderUI.SetActive(true);
        displayText.text = text.TextToDisplay;
        titleText.text = text.title;
        playerManager.ChangePlayerState(PlayerState.fullyPaused);

    }

    public void ResumeGame()
    {
        playerIsReading = false;
        playerManager.ChangePlayerState(PlayerState.freeMovement);
        ReaderUI.SetActive(false);
        displayText.text = "";

    }

}
