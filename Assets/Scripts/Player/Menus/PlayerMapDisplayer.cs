using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMapDisplayer : MonoBehaviour
{
    public GameObject mapObj;
    public Image map;
    public Vector3 offset;
    public Image canvas;

    public float minScale;
    public float maxScale;
    public Vector3 libray2;
    public float scrollMult;


    public Image[] mapMarkers;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float scroll = Input.mouseScrollDelta.y;

        if (map.transform.localScale.x + (scroll * scrollMult) > minScale && map.transform.localScale.x + (scroll * scrollMult) < maxScale)
        {

            map.transform.localScale += new Vector3(scroll * scrollMult, scroll * scrollMult, 0);
        }

    }

 

    public void ToggleMap(bool mapOn)
    {
        mapObj.SetActive(mapOn);

    }
    

}
