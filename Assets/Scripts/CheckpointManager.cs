using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CheckpointData
{
    public bool[] savedGameTriggers;
    public bool[] savedItemContainers;

    public float playerX;
    public float playerY;
    public float playerZ;

    
    public CheckpointData(bool[] _savedGameTriggers, bool[] _savedItemContainers , float _playerX, float _playerY, float _playerZ)
    {
        this.savedGameTriggers = _savedGameTriggers;
        this.savedItemContainers = _savedItemContainers;
        playerX = _playerX;
        playerY = _playerY;
        playerZ = _playerZ;
    }
}

public class CheckpointManager : MonoBehaviour
{

    public AbstractGameEventTrigger[]   globalGameTriggers;
    public ItemContainer[]              globalItemContainers;
    public bool itemAtIndexIsGone;
    public bool eventAtIndexIsTriggered;

    public Transform player;


    private void Start()
    {
        player = GameObject.Find("Player").transform;

        globalItemContainers = GetComponentsInChildren<ItemContainer>();
        globalGameTriggers = GetComponentsInChildren<AbstractGameEventTrigger>();

    }
    public CheckpointData SaveState()
    {
        bool[] triggerHasBeenTriggered = new bool[globalGameTriggers.Length] ;
        for(int i = 0; i < globalGameTriggers.Length; i++)
        {
            triggerHasBeenTriggered[i] = globalGameTriggers[i].hasBeenTriggered;

        }

        bool[] itemHasBeenPickedUp = new bool[globalItemContainers.Length];
        for(int i = 0; i < globalItemContainers.Length; i++)
        {
            itemHasBeenPickedUp[i] = globalItemContainers[i].pickedup;
        }



        CheckpointData data = new CheckpointData(triggerHasBeenTriggered, itemHasBeenPickedUp, player.transform.position.x, player.transform.position.y, player.transform.position.z );

        return data;
    }
    public void LoadState(CheckpointData data)
    {
        for (int i = 0; i < globalGameTriggers.Length; i++)
        {
            if (data.savedGameTriggers[i])
            {
                globalGameTriggers[i].TriggerEvent();
                
            }
        }

        for (int i = 0; i < globalItemContainers.Length; i++)
        {
            if (data.savedItemContainers[i])
            {
                globalItemContainers[i].PickUpItem();

            }
        }

        player.transform.position = new Vector3(data.playerX, data.playerY, data.playerZ);
    }
}
