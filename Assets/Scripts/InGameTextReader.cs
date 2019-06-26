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

    public void Start()
    {
        returnButton.onClick.AddListener(() => ResumeGame());
        ReaderUI.SetActive(false);
        controller = GetComponent<FirstPersonCharacterController>();
    }

    public void DisplayText(InGameText text)
    {
        ReaderUI.SetActive(true);
        displayText.text = text.TextToDisplay;
        controller.characterIsActive = false;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        mouseLook.cameraControl = false;
        gameCamera.cameraIsActive = false;


    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        ReaderUI.SetActive(false);
        displayText.text = "";
        controller.characterIsActive = true;
        gameCamera.cameraIsActive = true;
        Cursor.lockState = CursorLockMode.Locked;
        mouseLook.cameraControl = true;

    }
    
}
