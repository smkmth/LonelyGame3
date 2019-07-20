using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InGameTextReader : MonoBehaviour
{

    public GameObject ReaderUI;
    public TextMeshProUGUI displayText;
    public FirstPersonCharacterController controller;
    public SmoothMouseLook mouseLook;
    public Button returnButton;
    public InGameCamera gameCamera;
    public TextDisplayer textDisplayer;

    public PlayerManager playerManager;

    private float timer;

    public float inputInactive =.3f;

    private bool canClick = false;
    public bool playerIsReading =false;

    public void Start()
    {
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
        textDisplayer.ToggleTextDisplay(true);

    }
    public void DisplayText(InGameText text)
    {
        playerIsReading = true;
        canClick = false;
        timer = inputInactive;
        ReaderUI.SetActive(true);
        displayText.text = text.TextToDisplay;
        //playerManager.ChangePlayerState(PlayerState.fullyPaused);

    }

    public void ResumeGame()
    {
        playerIsReading = false;
        playerManager.ChangePlayerState(PlayerState.freeMovement);
        ReaderUI.SetActive(false);
        displayText.text = "";

    }

}
