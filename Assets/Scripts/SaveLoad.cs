using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

//this is the file to actually save
[System.Serializable]
public class SaveData
{
    public SettingsData settings;

}

//binary formatter for saving and loading game settings 
public class SaveLoad : MonoBehaviour
{
    private InGameSettings settingsManager;

    //on start, get ingamesettings, then try to load the game
    private void Start()
    {
        settingsManager = GetComponent<InGameSettings>();
        LoadGame();
    }

    //this function is the io operation which saves a file to the persistant data path
    public void SaveGame()
    {
        SaveData save = CreateSaveGameObject();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();

    }

    //this function actually pulls the data out of the game and cashes it in 
    // a format the binary file formater can read
    private SaveData CreateSaveGameObject()
    {
        SaveData save = new SaveData();

        save.settings = settingsManager.GetSettingsData();

        return save;
    }

    //this function checks if a save exists, and then loads that save, if it doesnt exist
    //it reports that back.
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
