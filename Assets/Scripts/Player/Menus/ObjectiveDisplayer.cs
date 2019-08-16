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
    public List<GameObjective> inumObjectiveList;
    public Dictionary<GameObjective,GameObject> objectiveObjList = new Dictionary<GameObjective, GameObject>();

    public GameObject objectiveViewer;
    public GameObject objectiveMenu;
    public GameObjective startObjective;
    public TextMeshProUGUI objectiveText;
    public TextMeshProUGUI objectiveTitle;

    public GameObject onScreenObjectiveView;
    public TextMeshProUGUI onScreenObjective;
    public float onScreenObjectiveTime;
    private float objectiveTimer;
    private bool showingObjective;
    public Image onScreenObjectiveImage;
    public AudioSource source;
    public AudioClip objectiveSound;
    public AudioClip transitionSound;
    public float objectiveVol;

    public void Start()
    {
        onScreenObjectiveView.SetActive(false);
        //AddObjective(startObjective);
    }

    public void ToggleObjectivesMenu(bool objectivesOn)
    {
        objectiveMenu.SetActive(true);
        objectivePanel.SetActive(objectivesOn);
        objectiveViewer.SetActive(false);

    }

    public string[] GetObjectiveList()
    {
        string[] obj = new string[inumObjectiveList.Count];
        for (int i = 0; i < inumObjectiveList.Count; i++)
        {
            obj[i] = inumObjectiveList[i].objectiveName;

        }

        return obj;
    }

    public void AddObjective(GameObjective objectiveToAdd)
    {
        foreach(GameObjective objective in inumObjectiveList)
        {
            if (objectiveToAdd.objectiveName == objective.objectiveName)
            {
                Debug.Log("already added " + objectiveToAdd);
                return;
            }
        }
        GameObject obj = Instantiate(objectivePrefab, objectiveScrollRect);
        obj.transform.GetChild(0).GetComponent<Text>().text = objectiveToAdd.objectiveName;
        obj.transform.GetChild(1).GetComponent<Text>().text = "In Progress";
        objectiveObjList.Add(objectiveToAdd, obj);
        obj.GetComponent<Button>().onClick.AddListener(() => ViewObjective(objectiveToAdd));
        onScreenObjectiveView.SetActive(true);
        onScreenObjective.text = "New Objective : " + objectiveToAdd.objectiveName;
        showingObjective = true;
        objectiveTimer = onScreenObjectiveTime;
        inumObjectiveList.Add(objectiveToAdd);
        source.PlayOneShot(objectiveSound, objectiveVol);
        StartCoroutine(FadeImage(false, onScreenObjectiveImage));
        StartCoroutine(FadeText(false, onScreenObjective));



    }
    public void UpdateObjective(GameObjective objectiveToUpdate, GameObjective objectiveToUpdateWith)
    {
        GameObject objectiveObj;
        if (objectiveObjList.TryGetValue(objectiveToUpdate, out objectiveObj))
        {

        }
        if(!objectiveObj)
        {
            AddObjective(objectiveToUpdate);
            objectiveObjList.TryGetValue(objectiveToUpdate, out objectiveObj);


        }
        onScreenObjectiveView.SetActive(true);
        onScreenObjective.text = "Objective Updated : " + objectiveToUpdate.objectiveName;
        showingObjective = true;
        objectiveTimer = onScreenObjectiveTime;
         objectiveObj.GetComponent<Button>().onClick.AddListener(() => ViewObjective(objectiveToUpdateWith));

        inumObjectiveList.Remove(objectiveToUpdate);
        inumObjectiveList.Add(objectiveToUpdateWith);
        source.PlayOneShot(objectiveSound, objectiveVol);

        StartCoroutine(FadeImage(false, onScreenObjectiveImage));
        StartCoroutine(FadeText(false, onScreenObjective));




    }

    public void FinishObjective(GameObjective objectiveToFinish)
    {
        GameObject objectiveObj;
        if (objectiveObjList.TryGetValue(objectiveToFinish, out objectiveObj))
        {

            objectiveObj.transform.GetChild(1).GetComponent<Text>().text = "Completed!";
        }
        onScreenObjectiveView.SetActive(true);
        onScreenObjective.text = "Objective Complete : " + objectiveToFinish.objectiveName;
        showingObjective = true;
        objectiveTimer = onScreenObjectiveTime;
        StartCoroutine(FadeImage(false, onScreenObjectiveImage));
        StartCoroutine(FadeText(false, onScreenObjective));
        source.PlayOneShot(objectiveSound, objectiveVol);


    }

    public void ViewObjective(GameObjective objective)
    {
        source.PlayOneShot(transitionSound, 1);
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


    public void Update()
    {
        if (showingObjective)
        {
            objectiveTimer -= Time.deltaTime;
            if (objectiveTimer <= 0)
            {
                    StartCoroutine(FadeImage(true, onScreenObjectiveImage));
                    StartCoroutine(FadeText(true, onScreenObjective));
                //onScreenObjectiveView.SetActive(false);
                    showingObjective = false;

                
            }
        }
       
    }


    IEnumerator FadeImage(bool fadeAway, Image image)
    {
        if (fadeAway)
        {
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
       
                image.color = new Color(255, 254, 201, i);

                yield return null;
                image.gameObject.SetActive(false);
            }
        }
        else
        {
            image.gameObject.SetActive(true);

            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                image.color = new Color(255, 254, 201, i);
                yield return null;
            }
        }
    }
    IEnumerator FadeText(bool fadeAway, TextMeshProUGUI text)
    {
        if (fadeAway)
        {
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                text.color = new Color(255, 254, 201, i);
                yield return null;
                text.gameObject.SetActive(false);
            }
        }
        else
        {
            text.gameObject.SetActive(true);
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                text.color = new Color(255, 254, 201, i);
                yield return null;

            }

        }
    }
}

