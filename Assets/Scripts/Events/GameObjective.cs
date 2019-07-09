using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ObjectiveType
{
    MainObjective,
    SubObjective
}
[CreateAssetMenu(menuName = "Objective")]
public class GameObjective : ScriptableObject 
{
    public string objectiveString;
    public string objectiveName;
    public GameObjective mainObjective;
    public ObjectiveType objectiveType;

}
