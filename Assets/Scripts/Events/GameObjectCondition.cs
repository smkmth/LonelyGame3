using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectCondition : Condition
{
    public GameObject objectToCheck;

    public override bool CheckCondition()
    {
        if (objectToCheck.activeSelf)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
