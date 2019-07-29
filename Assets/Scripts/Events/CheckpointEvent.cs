using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointEvent : GameEventReceiver
{
    public Transform player;
    public CheckpointManager checkPointManager;

    public void Start()
    {
        player = GameObject.Find("Player").transform;
        checkPointManager = GameObject.Find("GameReset").GetComponent<CheckpointManager>();
    }
    public override void DoEvent()
    {
      
    }
}
