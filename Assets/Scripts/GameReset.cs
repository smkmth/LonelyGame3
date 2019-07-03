using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameReset : MonoBehaviour
{
    public PlayerManager playerManager;
    public GameObject gameOverScreen;


    public void ResetLevel()
    {
        Time.timeScale = 1.0f;
        gameOverScreen.SetActive(false);
        playerManager.ChangePlayerState(PlayerState.freeMovement);
        SceneManager.LoadScene("House");
    }
}
