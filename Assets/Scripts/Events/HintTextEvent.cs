using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("GameEvents/Hint Text Event")]
public class HintTextEvent : GameEventReceiver
{
    private PlayerManager player;

    public string hintToDisplay;
    public float lengthOfTimeToDisplayHint;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerManager>();
        
    }
    public override void DoEvent()
    {
        player.DisplayHint(hintToDisplay, lengthOfTimeToDisplayHint);
    }


}
