using System.Collections;
using System.Collections.Generic;
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

public class Setttings : MonoBehaviour
{
    public List<ResolutionData> resolutionDatas = new List<ResolutionData>();
    public SettingsData settingsData;

    // Start is called before the first frame update
    void Start()
    {
        Resolution[] resolutions = Screen.resolutions;
        foreach (var res in resolutions)
        {
            ResolutionData data = new ResolutionData(res.width , res.height ,res.refreshRate);
            resolutionDatas.Add(data);
        }
    }

    public void ChangeResolution(ResolutionData data)
    {
        Screen.SetResolution(data.screenWidth, data.screenHeight, true);
        settingsData.currentResolution = data;
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


}
