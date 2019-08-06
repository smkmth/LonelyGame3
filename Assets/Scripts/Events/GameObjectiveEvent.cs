using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ObjectiveEventType
{
    AddObjective,
    UpdateObjective,
    FinishObjective
}

[AddComponentMenu("GameEvents/Game Objective Event")]
public class GameObjectiveEvent : GameEventReceiver
{

    public ObjectiveEventType objectiveType;
    public GameObjective gameObjectiveToTrigger;
    public GameObjective gameObjectiveToUpdate;
    private ObjectiveDisplayer player;

    public void Start()
    {
        player = GameObject.Find("Player").GetComponent<ObjectiveDisplayer>();
    }

    public override void DoEvent()
    {
        switch (objectiveType)
        {
            case (ObjectiveEventType.AddObjective):
                player.AddObjective(gameObjectiveToTrigger);
                break;
            case (ObjectiveEventType.FinishObjective):
                player.FinishObjective(gameObjectiveToTrigger);
                break;
            case (ObjectiveEventType.UpdateObjective):
                player.UpdateObjective(gameObjectiveToTrigger, gameObjectiveToUpdate);

                break;

        }
    }
}
