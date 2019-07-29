using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

//this class is for casheing the currently available resolutions so UI can read them
//and we can save the players chosen resolution
[System.Serializable]
public class ResolutionData
{
    public int screenWidth;
    public int screenHeight;
    public int refreshRate;

    public ResolutionData(int _screenWidth, int _screenHeight, int _refreshRate)
    {
        screenWidth = _screenWidth;
        screenHeight = _screenHeight;
        refreshRate = _refreshRate;
    }
}

//this is a class for storing all the settings that can change together so
//they can be saved
[System.Serializable]
public class SettingsData
{
    public ResolutionData currentResolution;
    public int currentQualitySetting;
}

//This class is responcible for changing game settings, and also casheing those
//saved settings and saving them. All this functionality is in this one class
// to prevent lots of IO operations - this one class can keep an open cashe to 
//all the settings, and save them whenever is conviniant 
public class InGameSettings : MonoBehaviour
{

    public List<ResolutionData> resolutionDatas = new List<ResolutionData>();
    public SettingsData settingsData;
    public SaveLoad saveLoad;

    //On awake, step through all the screen resolutions available and add them to
    //our list of resolutions 
    void Awake()
    {
        saveLoad = GetComponent<SaveLoad>();

        Resolution[] resolutions = Screen.resolutions;
        foreach (Resolution res in resolutions)
        {
            ResolutionData data = new ResolutionData(res.width, res.height, res.refreshRate);
            resolutionDatas.Add(data);
        }
    }

    
    public SettingsData GetSettingsData()
    {
        return settingsData;
    }

    //here we save the actual game, i wrote this here so the UI for settings can just use 
    //this function in a callback.
    public void SaveSettingData()
    {
        saveLoad.SaveGame();
    }

    //Sets the resolution to the chosen resolutiondata 
    public void ChangeResolution(ResolutionData data)
    {
        Screen.SetResolution(data.screenWidth, data.screenHeight, true);
        settingsData.currentResolution = data;
    }

    public int GetCurrentResolutionIndex()
    {
        int currentHeight = Screen.currentResolution.height;
        int currentWidth = Screen.currentResolution.width;
        for (int i = 0; i < resolutionDatas.Count; i++)
        {
            if (resolutionDatas[i].screenHeight == currentHeight)
            {
                if (resolutionDatas[i].screenWidth == currentWidth)
                {
                    return i;
                }
            }

        }
        return -1;

    }
    public void IncreaseQualitySetting()
    {
        int qualityLevel = QualitySettings.GetQualityLevel();
        QualitySettings.SetQualityLevel(qualityLevel++);
        settingsData.currentQualitySetting = qualityLevel;
    }

    public void DecreaseQualitySetting()
    {
        int qualityLevel = QualitySettings.GetQualityLevel();
        QualitySettings.SetQualityLevel(qualityLevel--);
        settingsData.currentQualitySetting = qualityLevel;
    }

    public void LoadSettings(SettingsData data)
    {
        ChangeResolution(data.currentResolution);
        QualitySettings.SetQualityLevel(data.currentQualitySetting);
    }

}