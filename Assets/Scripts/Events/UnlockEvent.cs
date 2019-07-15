using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("GameEvents/Unlock Event")]
public class UnlockEvent : GameEventReceiver
{
    public GameEventTrigger doorToUnlock;

    public override void DoEvent()
    {
        doorToUnlock.locked = false;
    }
}
