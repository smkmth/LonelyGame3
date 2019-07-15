using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEvent : GameEventReceiver
{
    public ParticleSystem particalSystem;

    public override void DoEvent()
    {
        particalSystem.Play();
    }
}
