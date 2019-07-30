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
    public PlayerData playerData;
    public CheckpointData worldData;

}

//binary formatter for saving and loading game settings 
public class SaveLoad : MonoBehaviour
{
    private InGameSettings settingsManager;
    private PlayerManager player;
    private CheckpointManager checkpointManager;
    //on start, get ingamesettings, then try to load the game
    private void Start()
    {
        settingsManager = GetComponent<InGameSettings>();
       // LoadGame();
    }


    

    //this function is the io operation which saves a file to the persistant data path
    public void SaveGame()
    {
        SaveData save = CreateSaveGameObject();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.giz");
        bf.Serialize(file, save);
        file.Close();

    }

 


    //this function actually pulls the data out of the game and cashes it in 
    // a format the binary file formater can read
    private SaveData CreateSaveGameObject()
    {
        SaveData save = new SaveData();


        player = GameObject.Find("Player").GetComponent<PlayerManager>();
        checkpointManager = GameObject.Find("LevelStateManager").GetComponent<CheckpointManager>();

        save.worldData = checkpointManager.SaveState();
        save.playerData = player.SavePlayer();
        save.settings = settingsManager.GetSettingsData();

        return save;
    }

    //this function checks if a save exists, and then loads that save, if it doesnt exist
    //it reports that back.
    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.giz"))
        {
            player = GameObject.Find("Player").GetComponent<PlayerManager>();
            checkpointManager = GameObject.Find("LevelStateManager").GetComponent<CheckpointManager>();


            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.giz", FileMode.Open);
            SaveData save = (SaveData)bf.Deserialize(file);
            file.Close();
            settingsManager.LoadSettings(save.settings);

        }
        else
        {
            Debug.Log("No game saved!");
        }
    }
    //this function checks if a save exists, and then loads that save, if it doesnt exist
    //it reports that back.
    public void LoadPlayer()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.giz"))
        {
            player = GameObject.Find("Player").GetComponent<PlayerManager>();
            checkpointManager = GameObject.Find("LevelStateManager").GetComponent<CheckpointManager>();


            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.giz", FileMode.Open);
            SaveData save = (SaveData)bf.Deserialize(file);
            file.Close();
            player.LoadPlayer(save.playerData);
            checkpointManager.LoadState(save.worldData);
        }
        else
        {
            Debug.Log("No game saved!");
        }
    }

}
