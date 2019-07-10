using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockEvent : GameEventReceiver
{
    public GameEventTrigger doorToUnlock;

    public override void DoEvent()
    {
        doorToUnlock.locked = false;
    }
}
