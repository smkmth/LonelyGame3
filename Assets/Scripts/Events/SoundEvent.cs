using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SoundEffectType
{
    OneShot,
    LoopConstant,
    LoopForTime,
    StopLoop
}

[AddComponentMenu("GameEvents/Sound Event")]
public class SoundEvent : GameEventReceiver {

    public AudioClip audioClip;
    public AudioSource placeSoundComesFrom;
    public float volumeSetting;
    public SoundEffectType typeOfSound;
    float timer;
    bool timing;
    bool waitToStopLoop;

    public override void DoEvent()
    {
        switch (typeOfSound)
        {
            case SoundEffectType.OneShot:
                placeSoundComesFrom.PlayOneShot(audioClip, volumeSetting);
                break;
            case SoundEffectType.LoopConstant:
                placeSoundComesFrom.clip = audioClip;
                placeSoundComesFrom.loop = true;
                placeSoundComesFrom.Play();
                break;
            case SoundEffectType.LoopForTime:
                placeSoundComesFrom.loop = true;
                placeSoundComesFrom.Play();
                break;
            case SoundEffectType.StopLoop:
                placeSoundComesFrom.loop =false;
                break;
        }
    }

    public void Update()
    {
        if (timing)
        {
            timer -= Time.deltaTime;
            if (timer <=0)
            {
                placeSoundComesFrom.Stop();
            }

        }
       
        
    }
}
