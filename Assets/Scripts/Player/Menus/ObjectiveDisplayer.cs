using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ObjectiveDisplayer : MonoBehaviour
{

    public GameObject objectivePrefab;
    public Transform objectiveScrollRect;
    public GameObject objectivePanel;
    public Dictionary<GameObjective,GameObject> objectiveObjList = new Dictionary<GameObjective, GameObject>();

    public GameObjective startObjective;

    public void Start()
    {
        AddObjective(startObjective);
    }

    public void ToggleObjectivesMenu(bool objectivesOn)
    {
        objectivePanel.SetActive(objectivesOn);

    }

    public void AddObjective(GameObjective objectiveToAdd)
    {
        GameObject obj = Instantiate(objectivePrefab, objectiveScrollRect);
        obj.transform.GetChild(0).GetComponent<Text>().text = objectiveToAdd.objectiveString;
        obj.transform.GetChild(1).GetComponent<Text>().text = "In Progress";
        objectiveObjList.Add(objectiveToAdd, obj);
        
      
        
    }

    public void FinishObjective(GameObjective objectiveToFinish)
    {
        GameObject objectiveObj;
        if (objectiveObjList.TryGetValue(objectiveToFinish, out objectiveObj))
        {
            objectiveObj.transform.GetChild(1).GetComponent<Text>().text = "Completed!";
        }

    }

}

