using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public SettingsData settings;

   
}

public class SaveLoad : MonoBehaviour
{
    public InGameSettings settingsManager;

    private void Start()
    {
        settingsManager = GetComponent<InGameSettings>();
    }
    private SaveData CreateSaveGameObject()
    {
        SaveData save = new SaveData();

        save.settings = settingsManager.GetSettingsData();

        return save;
    }
    public void SaveGame()
    {
        SaveData save = CreateSaveGameObject();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();

    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            SaveData save = (SaveData)bf.Deserialize(file);
            file.Close();
            settingsManager.LoadSettings(save.settings);
        }
        else
        {
            Debug.Log("No game saved!");
        }
    }
}
