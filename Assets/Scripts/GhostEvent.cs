using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GhostEventType
{
    ActivateGhost,
    MoveGhost
}
[CreateAssetMenu(menuName = "GhostEvent")]
public class GhostEvent : ScriptableEvent
{
    public GhostEventType thisGhostEvent;
    public Vector3 ghostTarget;

}
