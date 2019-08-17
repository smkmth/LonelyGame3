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

    public Transform exposureVolume;
    public Slider brightnessSlider;
    public float maxBrightness;
    public float minBrightness;


    public float volume;
    public Text volumeText;
    public float maxVolume;
    public float minVolume;
    public Slider volSlider;

    public float defaultVol;
    public float defaultBrightness;

    private void Start()
    {
        volSlider.maxValue = maxVolume;
        volSlider.minValue = minVolume;
        volSlider.value = AudioListener.volume;

        brightnessSlider.maxValue = maxBrightness;
        brightnessSlider.minValue = minBrightness;
        brightnessSlider.value = Mathf.Clamp( exposureVolume.transform.localPosition.y, maxBrightness, minBrightness);

        defaultVol = AudioListener.volume;
        defaultBrightness = exposureVolume.transform.localPosition.y;
    }

    public void ToggleSettingsDisplay(bool isSettingsMenu)
    {
        settings = GameObject.Find("GameReset").GetComponent<InGameSettings>();
        settingsMenu.SetActive(isSettingsMenu);
        currentResolutionIndex = settings.GetCurrentResolutionIndex();
        resolutionText.text = settings.resolutionDatas[currentResolutionIndex].screenHeight.ToString() + " X " + settings.resolutionDatas[currentResolutionIndex].screenWidth.ToString();

       // brightnessSlider.value  = Mathf.Clamp(exposureVolume.transform.position.y, maxBrightness, minBrightness);
        volSlider.value         = AudioListener.volume;

    }



    public void SetBrightness()
    {
        exposureVolume.transform.localPosition = new Vector3(0,brightnessSlider.value, 0);


    }

    public void SetVolume()
    {
       AudioListener.volume = volSlider.value;
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

    public void RevertSettings()
    {
        AudioListener.volume = defaultVol;
        exposureVolume.transform.localPosition = new Vector3(0, defaultBrightness, 0);

    }
}
