using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsDisplayer : MonoBehaviour
{

    public GameObject settingsMenu;
    public int currentResolutionIndex;
    public Text resolutionText;
    private InGameSettings settings;
    public float brightness;


    private void Start()
    {
    }

    public void ToggleSettingsDisplay(bool isSettingsMenu)
    {
        settings = GameObject.Find("GameReset").GetComponent<InGameSettings>();
        settingsMenu.SetActive(isSettingsMenu);
        currentResolutionIndex = settings.GetCurrentResolutionIndex();
        resolutionText.text = settings.resolutionDatas[currentResolutionIndex].screenHeight.ToString() + " X " + settings.resolutionDatas[currentResolutionIndex].screenWidth.ToString();

    }

    public void SetBrightness()
    {

    } 


    public void SelectNextResolution()
    {
        if ((currentResolutionIndex+1) < settings.resolutionDatas.Count)
        {
            currentResolutionIndex++;
            resolutionText.text = settings.resolutionDatas[currentResolutionIndex].screenHeight.ToString() + " X " + settings.resolutionDatas[currentResolutionIndex].screenWidth.ToString();
        }
    }

    public void SelectPreviousResolution()
    {
        if ((currentResolutionIndex - 1) >=0)
        {
            currentResolutionIndex--;
            resolutionText.text = settings.resolutionDatas[currentResolutionIndex].screenHeight.ToString() + " X " + settings.resolutionDatas[currentResolutionIndex].screenWidth.ToString();
        }
    }

    public void ConfirmSettings()
    {
        settings.ChangeResolution(settings.resolutionDatas[currentResolutionIndex]);
        settings.SaveSettingData();
    }
}
