
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{

    public Item heldItem;
    public bool pickedup;

    private void Start()
    {
        if (!heldItem)
        {
            Debug.Log(name);
        }
        
        name = heldItem.title;
    }

    public void PickUpItem()
    {
        pickedup = true;
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
    }
}
