using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveDisplayer : MonoBehaviour
{

    public GameObject objectivePrefab;
    public Transform objectiveScrollRect;
    public GameObject objectivePanel;
    public List<GameObject> objectiveObjList;


    public void ToggleObjectivesMenu(bool objectivesOn)
    {
        objectivePanel.SetActive(objectivesOn);

    }

    public void AddObjective(GameObjective objectiveToAdd)
    {
        if (objectiveToAdd.objectiveType == ObjectiveType.MainObjective)
        {
            foreach(GameObject objectiveObj in objectiveObjList)
            {
                if (objectiveObj.activeSelf == false)
                {
                    objectiveObj.SetActive(true);
                    objectiveObj.GetComponent<ObjectiveContainer>().mainObjective.text = objectiveToAdd.objectiveString;
                    objectiveObj.name = objectiveToAdd.objectiveName;
                    return;
                }
            }
            

        }
        else if(objectiveToAdd.objectiveType == ObjectiveType.SubObjective)
        {
            foreach (GameObject objective in objectiveObjList)
            { 
                if (objective.name == objectiveToAdd.mainObjective.objectiveName)
                {
                    TextMeshProUGUI[] subObjectiveObjs = objective.GetComponent<ObjectiveContainer>().subObjectives;
                    foreach(TextMeshProUGUI subOjectiveObj in subObjectiveObjs)
                    {
                        if (subOjectiveObj.gameObject.activeSelf == false)
                        {
                            subOjectiveObj.gameObject.SetActive(true);
                            subOjectiveObj.text = objectiveToAdd.objectiveString;
                            return;

                        }
                    }
                }

            }
        }
    }
   
}

