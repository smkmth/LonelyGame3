using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType
{
    Sound,
    Animation,
    Debug,
    GhostEvent,
    GameWinEvent

}

public class ScriptableEvent : ScriptableObject
{
    public EventType eventType;

}
