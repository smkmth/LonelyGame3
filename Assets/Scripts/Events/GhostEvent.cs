﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GhostEventType
{
    ActivateGhost,
    MoveGhost,
    TeleportGhost,
    DeActivateGhost,
    SetPlayerInvisible
}

[AddComponentMenu("GameEvents/Ghost Event")]
public class GhostEvent : GameEventReceiver 
{
    public Ghost ghostToBeAffected;
    public GhostEventType thisGhostEvent;
    public Transform ghostTarget;
    public bool playerInvisible;

    public override void DoEvent()
    {
        switch (thisGhostEvent)
        {
            case (GhostEventType.ActivateGhost):
                ghostToBeAffected.gameObject.SetActive(true);
                ghostToBeAffected.OnFirstActivateGhost();
                break;
            case (GhostEventType.MoveGhost):
                ghostToBeAffected.gotToPlaceTarget = ghostTarget;
                ghostToBeAffected.ChangeGhostState(GhostState.GoToPlace);
                break;
            case (GhostEventType.TeleportGhost):
                ghostToBeAffected.gameObject.transform.position = ghostTarget.position;
                break;
            case (GhostEventType.DeActivateGhost):
                ghostToBeAffected.ghostActive = false;
                ghostToBeAffected.gameObject.SetActive(false);
                break;
            case (GhostEventType.SetPlayerInvisible):
                ghostToBeAffected.playerIsInvisible = playerInvisible;
                break;

        }
    }
}
