using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

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

[System.Serializable]
public class SettingsData
{
    public ResolutionData currentResolution;
    public int currentQualitySetting;
}


public class InGameSettings : MonoBehaviour
{

    public static string file = "GameData.dat";
    public static ArrayList Data = new ArrayList();

    public List<ResolutionData> resolutionDatas = new List<ResolutionData>();
    public SettingsData settingsData;
    public SaveLoad saveLoad;
    // Start is called before the first frame update
    void Awake()
    {
        saveLoad = GetComponent<SaveLoad>();

        Resolution[] resolutions = Screen.resolutions;
        foreach (var res in resolutions)
        {
            ResolutionData data = new ResolutionData(res.width, res.height, res.refreshRate);
            resolutionDatas.Add(data);
        }
    }

    public SettingsData GetSettingsData()
    {
        return settingsData;
    }

    public void SaveSettingData()
    {
        saveLoad.SaveGame();
    }


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