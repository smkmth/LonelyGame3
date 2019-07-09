using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveDisplayer : MonoBehaviour
{

    public GameObject objectivePrefab;
    public Transform objectiveScrollRect;
    public GameObject objectivePanel;
    public List<GameObject> objectiveList;
   
    public void AddObjective(GameObjective objectiveToAdd)
    {
        if (objectiveToAdd.objectiveType == ObjectiveType.MainObjective)
        {
            GameObject obj = Instantiate(objectivePrefab, objectiveScrollRect);
            objectiveList.Add( obj );
        }
        else if(objectiveToAdd.objectiveType == ObjectiveType.SubObjective)
        {
            foreach (GameObject objective in objectiveList)
            { 

            }
        }
    }
   
}

