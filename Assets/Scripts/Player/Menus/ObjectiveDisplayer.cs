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

    public GameObject objectiveViewer;
    public GameObject objectiveMenu;
    public GameObjective startObjective;
    public TextMeshProUGUI objectiveText;
    public TextMeshProUGUI objectiveTitle;

    public void Start()
    {
        AddObjective(startObjective);
    }

    public void ToggleObjectivesMenu(bool objectivesOn)
    {
        objectiveMenu.SetActive(true);
        objectivePanel.SetActive(objectivesOn);
        objectiveViewer.SetActive(false);

    }

    public void AddObjective(GameObjective objectiveToAdd)
    {
        GameObject obj = Instantiate(objectivePrefab, objectiveScrollRect);
        obj.transform.GetChild(0).GetComponent<Text>().text = objectiveToAdd.objectiveName;
        obj.transform.GetChild(1).GetComponent<Text>().text = "In Progress";
        objectiveObjList.Add(objectiveToAdd, obj);
        obj.GetComponent<Button>().onClick.AddListener(() => ViewObjective(objectiveToAdd));



    }

    public void FinishObjective(GameObjective objectiveToFinish)
    {
        GameObject objectiveObj;
        if (objectiveObjList.TryGetValue(objectiveToFinish, out objectiveObj))
        {

            objectiveObj.transform.GetChild(1).GetComponent<Text>().text = "Completed!";
        }

    }

    public void ViewObjective(GameObjective objective)
    {
        objectiveMenu.SetActive(false);
        objectiveViewer.SetActive(true);
        objectiveTitle.text = objective.objectiveName;
        objectiveText.text= objective.objectiveString;


    }

    public void CloseObjective()
    {
        objectiveMenu.SetActive(true);

        ToggleObjectivesMenu(true);
        objectiveViewer.SetActive(false);
    }

}

