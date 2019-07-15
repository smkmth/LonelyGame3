using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckItem : Condition
{
    Inventory playerInventory;
    public Item requiredItems;
    public int numberNeeded;
    public void Start()
    {
        playerInventory = GameObject.Find("Player").GetComponent<Inventory>();
    }
    public override bool CheckCondition()
    {
        if (playerInventory.GetItemCount(requiredItems) >= numberNeeded)
        {
            return true;
        }
        return false;

    }

 
}
