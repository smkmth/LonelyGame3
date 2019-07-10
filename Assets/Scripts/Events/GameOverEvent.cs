using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverEvent : GameEventReceiver
{
    private PlayerManager playerManager;

    public override void DoEvent()
    {
        playerManager.WinGame();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
        
    }


}
