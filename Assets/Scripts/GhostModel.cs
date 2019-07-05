using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostModel : MonoBehaviour
{
    public Cloth[] cloths;

    public float clothTime;
    public float timer;
    public bool clothMoving;
    public Transform ghostController;
    
    public void FreezePos()
    {
        transform.parent = null;
        foreach (Cloth cloth in cloths)
        {
            cloth.damping= 1;
        }
    }
    public void UnFreezePos()
    {
        transform.parent = ghostController;
        foreach (Cloth cloth in cloths)
        {
            cloth.damping = 0.2f;
        }
    }
}

