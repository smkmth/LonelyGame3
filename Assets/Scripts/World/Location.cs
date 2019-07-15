using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{

    public string location;

    public bool isPlayerHere;
    public Vector3 mapPos;
    public PlayerMapDisplayer map;
    public void Start()
    {
        map = GameObject.Find("Player").GetComponent<PlayerMapDisplayer>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.name == "PlayerColliderObj")
        {
            map.SetMapPos(this);
            isPlayerHere = true;
            Debug.Log("Player is at " + location);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.name == "PlayerColliderObj")
        {
            isPlayerHere = false;
            Debug.Log("Player has left " + location);
        }
    }
}
