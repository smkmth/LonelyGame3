using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointEvent : GameEventReceiver
{
    public Transform player;
    public SaveLoad saveData;

    public void Start()
    {
        player = GameObject.Find("Player").transform;

        saveData = GameObject.Find("GameReset").GetComponent<SaveLoad>();
    }
    public override void DoEvent()
    {
        saveData.SaveGame();
      
    }
}
