using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameOverType
{
    Win, 
    Death
}

[AddComponentMenu("GameEvents/Game Over Event")]
public class GameOverEvent : GameEventReceiver
{
    private PlayerManager playerManager;
    public GameOverType gameOverType;



    public override void DoEvent()
    {
        switch (gameOverType)
        {
            case GameOverType.Win:
                playerManager.WinGame();
                break;
            case GameOverType.Death:
                playerManager.GameLose();
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
        
    }


}
