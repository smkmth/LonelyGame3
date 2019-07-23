
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{

    public Item heldItem;
    private void Start()
    {
        if (!heldItem)
        {
            Debug.Log(name);
        }
        
        name = heldItem.title;
    }
}
