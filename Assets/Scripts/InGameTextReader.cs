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
    public Button returnButton;

    public void Start()
    {
        returnButton.onClick.AddListener(() => ResumeGame());

        controller = GetComponent<FirstPersonCharacterController>();
    }

    public void DisplayText(InGameText text)
    {
        ReaderUI.SetActive(true);
        displayText.text = text.TextToDisplay;
        controller.characterIsActive = false;
        Time.timeScale = 0;


    }

    public void ResumeGame()
    {
        ReaderUI.SetActive(false);
        controller.characterIsActive = true;
        Time.timeScale = 0;

    }
    
}
