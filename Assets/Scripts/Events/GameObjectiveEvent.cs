using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectiveEvent : GameEventReceiver
{
    public GameObjective gameObjectiveToTrigger;
    private ObjectiveDisplayer player;

    public void Start()
    {
        player = GameObject.Find("Player").GetComponent<ObjectiveDisplayer>();
    }

    public override void DoEvent()
    {
        player.AddObjective(gameObjectiveToTrigger);
    }
}
