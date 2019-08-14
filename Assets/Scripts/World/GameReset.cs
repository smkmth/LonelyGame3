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
    public string menuScene;
    public SaveLoad load;


    public InGameText startText;
    public InGameText manualText;
    public TextDisplayer textDisplayer;

    private void Start()
    {
        load = GetComponent<SaveLoad>();
    }

    public void QuitGameToMenu()
    {
        loadingScreen.SetActive(true);

        Time.timeScale = 1.0f;
        //load.LoadPlayer();
        StartCoroutine(QuitToMenu());

    }
    public void ResetLevel()
    {
        loadingScreen.SetActive(true);

        Time.timeScale = 1.0f;
        //load.LoadPlayer();
        StartCoroutine(ReloadScene());

    }
    public void LoadNewScene()
    {
        loadingScreen.SetActive(true);

        Time.timeScale = 1.0f;

        StartCoroutine(InitLoadScene());

    }
    IEnumerator ReloadScene()
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

        loadingScreen.GetComponent<Image>().CrossFadeAlpha(0.0f, 2.0f, false);
        yield return new WaitForSeconds(2.1f);
        loadingScreen.SetActive(false);
        yield return null;
    }
    IEnumerator QuitToMenu()
    {

        AsyncOperation async = SceneManager.LoadSceneAsync(firstScene, LoadSceneMode.Additive);
        async = SceneManager.UnloadSceneAsync(firstScene);
        async = SceneManager.UnloadSceneAsync(secondScene);
        async = SceneManager.UnloadSceneAsync(thirdScene);
        async = SceneManager.UnloadSceneAsync(playerScene);

        async = SceneManager.LoadSceneAsync(menuScene, LoadSceneMode.Additive);
;

        yield return new WaitForEndOfFrame();
        while (!async.isDone)
        {
            yield return null;
        }

        //after scene is loaded - wait for 2 seconds for the player to fall a bit - and everything to kind of shuffle in 


        yield return new WaitForSeconds(2f);

        loadingScreen.GetComponent<Image>().CrossFadeAlpha(0.0f, 2.0f, false);
        yield return new WaitForSeconds(2.1f);
        loadingScreen.SetActive(false);
        yield return null;
    }

    IEnumerator InitLoadScene()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(firstScene, LoadSceneMode.Additive);
        async = SceneManager.UnloadSceneAsync(menuScene);
  
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

        loadingScreen.GetComponent<Image>().CrossFadeAlpha(0.0f, 1.0f, false);
        yield return new WaitForSeconds(1.1f);
        loadingScreen.SetActive(false);
        textDisplayer = GameObject.Find("Player").GetComponent<TextDisplayer>();
        InGameTextReader reader = GameObject.Find("Player").GetComponent<InGameTextReader>();
        textDisplayer.AddTextAsset(startText);
        textDisplayer.AddTextAsset(manualText);
        reader.DisplayText(startText, false);

        // loadingScreen.SetActive(false);
        yield return null;
    }
}
