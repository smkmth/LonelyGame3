using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameReset : MonoBehaviour
{
    public PlayerManager playerManager;
    public GameObject loadingScreen;
    public string firstScene;
    public string secondScene;
    public string thirdScene;
    public string playerScene;
    public SaveLoad load;

    private void Start()
    {
        load = GetComponent<SaveLoad>();
    }


    public void ResetLevel()
    {
        Time.timeScale = 1.0f;
        StartCoroutine(LoadNewScene());

    }

    IEnumerator LoadNewScene()
    {
        loadingScreen.SetActive(true);
        load.LoadPlayer();

        /*
        yield return new WaitForEndOfFrame();
        AsyncOperation async = SceneManager.LoadSceneAsync(firstScene, LoadSceneMode.Additive);
        async = SceneManager.LoadSceneAsync(secondScene, LoadSceneMode.Additive);
        async = SceneManager.LoadSceneAsync(thirdScene, LoadSceneMode.Additive);
        async = SceneManager.LoadSceneAsync(playerScene, LoadSceneMode.Additive);

        while (!async.isDone)
        {
            yield return null;
        }

        //after scene is loaded - wait for 2 seconds for the player to fall a bit - and everything to kind of shuffle in 
        */

        yield return new WaitForSeconds(0.1f);
        loadingScreen.SetActive(false);
    }
}
