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

    public PlayerManager playerManager;

    private float timer;

    public float inputInactive =.3f;

    private bool canClick = false;

    public void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        returnButton.onClick.AddListener(() => ResumeGame());
        ReaderUI.SetActive(false);
    }

    private void Update()
    {
        if (timer <= 0)
        {
            canClick = true;
        }
        else
        {
            timer -= Time.unscaledDeltaTime;
        }


        if (Input.GetButtonDown("Interact"))
        {

        }

        if (canClick)
        {
           

            if (Input.GetButtonDown("Interact"))
            {
                ResumeGame();
            }
        }
      
    }

    public void DisplayText(InGameText text)
    {
        Debug.Log("Display text");
        canClick = false;
        timer = inputInactive;
        ReaderUI.SetActive(true);
        displayText.text = text.TextToDisplay;
        playerManager.ChangePlayerState(PlayerState.fullyPaused);

    }

    public void ResumeGame()
    {

        playerManager.ChangePlayerState(PlayerState.freeMovement);
        ReaderUI.SetActive(false);
        displayText.text = "";


    }

}
