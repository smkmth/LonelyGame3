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
        //load.LoadPlayer();
        StartCoroutine(LoadNewScene());

    }

    IEnumerator LoadNewScene()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(firstScene, LoadSceneMode.Additive);
        async = SceneManager.UnloadSceneAsync(firstScene);
        async = SceneManager.UnloadSceneAsync(secondScene);
        async = SceneManager.UnloadSceneAsync(thirdScene);
        async = SceneManager.UnloadSceneAsync(playerScene);
        
        async = SceneManager.LoadSceneAsync(secondScene, LoadSceneMode.Additive);
        async = SceneManager.LoadSceneAsync(thirdScene, LoadSceneMode.Additive);
        async = SceneManager.LoadSceneAsync(playerScene, LoadSceneMode.Additive);

        yield return new WaitForEndOfFrame();
        while (!async.isDone)
        {
            yield return null;
        }

        //after scene is loaded - wait for 2 seconds for the player to fall a bit - and everything to kind of shuffle in 
        

        yield return new WaitForSeconds(2f);
        loadingScreen.SetActive(false);
        yield return null;
    }
}
