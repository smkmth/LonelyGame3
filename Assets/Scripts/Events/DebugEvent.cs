using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("GameEvents/Debug Event")]
public class DebugEvent : GameEventReceiver
{
    public string debugMessage;

   

    public override void DoEvent()
    {
        Debug.Log(debugMessage);
    }
}
