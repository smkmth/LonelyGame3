using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SoundEffectType
{
    OneShot,
    LoopConstant,
    LoopForTime
}
[AddComponentMenu("GameEvents/Sound Event")]
public class SoundEvent : GameEventReceiver {

    public AudioClip audioClip;
    public AudioSource placeSoundComesFrom;
    public float volumeSetting;
    public SoundEffectType typeOfSound;

    public override void DoEvent()
    {
        switch (typeOfSound)
        {
            case SoundEffectType.OneShot:
                placeSoundComesFrom.PlayOneShot(audioClip, volumeSetting);
                break;
            case SoundEffectType.LoopConstant:
                break;
            case SoundEffectType.LoopForTime:
                break;

        }
    }
}
