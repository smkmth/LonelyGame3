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
        gameCamera = Camera.main.gameObject.GetComponent<InGameCamera>();
    }

    public void DisplayText(InGameText text)
    {
        Time.timeScale = 0;
        ReaderUI.SetActive(true);
        displayText.text = text.TextToDisplay;
        mouseLook.cameraControl = false;
        Cursor.lockState = CursorLockMode.None;
        controller.characterIsActive = false;
        gameCamera.cameraIsActive = false;


    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        ReaderUI.SetActive(false);
        displayText.text = "";
        mouseLook.cameraControl = true;
        Cursor.lockState = CursorLockMode.Locked;
        controller.characterIsActive = true;
        gameCamera.cameraIsActive = true;

    }
    
}
