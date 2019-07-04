using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="SoundEvent")]
public class SoundEvent : GameEventReceiver {

    public AudioClip audioClip;
    public AudioSource placeSoundComesFrom;
    public float volumeSetting;

    public override void DoEvent()
    {
        placeSoundComesFrom.PlayOneShot(audioClip, volumeSetting);
    }
}
