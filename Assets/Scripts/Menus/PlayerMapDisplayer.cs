using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMapDisplayer : MonoBehaviour
{
    public GameObject mapObj;
    public Image playerPos;
    public Image map;
    public Vector3 offset;
    public Image canvas;

    public float minScale;
    public float maxScale;
    public Vector3 libray2;
    public float scrollMult;




    // Start is called before the first frame update
    void Start()
    {

        playerPos.GetComponent<RectTransform>().localPosition = new Vector3(525, 466, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float scroll = Input.mouseScrollDelta.y;

        if (playerPos.transform.localScale.x + (scroll * scrollMult) > minScale && playerPos.transform.localScale.x + (scroll * scrollMult) < maxScale)
        {

            playerPos.transform.localScale += new Vector3(scroll * scrollMult, scroll * scrollMult, 0);
            map.transform.localScale += new Vector3(scroll * scrollMult, scroll * scrollMult, 0);
        }




    }

    public void SetMapPos(Location location)
    {
        playerPos.GetComponent<RectTransform>().localPosition = location.mapPos;
    }

    public void ToggleMap(bool mapOn)
    {
        mapObj.SetActive(mapOn);

    }


}
