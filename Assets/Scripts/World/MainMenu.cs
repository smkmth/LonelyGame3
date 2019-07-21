﻿using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum MainMenuButtonType
{
    NewGameButton,
    LoadGameButton,
    SettingsButton,
    QuitGameButton
}

public class MainMenu : MonoBehaviour
{
    public GameObject pressAnyKey;
    public GameObject loadingScreen;
    public GameObject menuObject;
    public GameObject mainMenuObject;
    public GameObject settingsMenuObject;
    public SettingsDisplayer settingsMenu;

    public Button newGameButton;
    public Button loadGameButton;
    public Button settingsButton;
    public Button quitGameButton;

    public string playerScene;
    public string firstScene;
    public string secondScene;

    public GameObject logo;

    public TextMeshProUGUI explainationText;

    [TextArea]
    public string newGameText;
    [TextArea]
    public string loadGameText;
    [TextArea]
    public string settingsText;
    [TextArea]
    public string quitGameText;

    // Start is called before the first frame update
    void Start()
    {
        newGameButton.onClick.AddListener(() => ToggleMenu(MainMenuButtonType.NewGameButton));
        loadGameButton.onClick.AddListener(() => ToggleMenu(MainMenuButtonType.LoadGameButton));
        settingsButton.onClick.AddListener(() => ToggleMenu(MainMenuButtonType.SettingsButton));
        quitGameButton.onClick.AddListener(() => ToggleMenu(MainMenuButtonType.QuitGameButton));

    }
    public void GetHover(string hover)
    {
        Debug.Log(hover);
        if (hover == "NewGame")
        {
            explainationText.text = newGameText;

        }
        else if (hover == "LoadGame")
        {
            explainationText.text = loadGameText;

        }
        else if (hover == "Settings")
        {
            explainationText.text = settingsText;

        }
        else if (hover == "Quit")
        {
            explainationText.text = quitGameText;

        }
    }
    public void ToggleMenu(MainMenuButtonType menuTypeClicked)
    {
        switch (menuTypeClicked)
        {
            case MainMenuButtonType.NewGameButton:
                
                StartCoroutine(LoadNewScene());
                break;
            case MainMenuButtonType.LoadGameButton:
                break;
            case MainMenuButtonType.SettingsButton:
                logo.SetActive(false);
                mainMenuObject.SetActive(false);
                settingsMenu.ToggleSettingsDisplay(true);
                break;
            case MainMenuButtonType.QuitGameButton:
                Application.Quit();
                break;
        }
    }

    public void HoverMenu(MainMenuButtonType menuTypeClicked)
    {
        switch (menuTypeClicked)
        {
            case MainMenuButtonType.NewGameButton:
                break;
            case MainMenuButtonType.LoadGameButton:
                break;
            case MainMenuButtonType.SettingsButton:
                break;
            case MainMenuButtonType.QuitGameButton:
                break;
        }
    }

    public void ReturnToMainMenu()
    {
        logo.SetActive(true);
        mainMenuObject.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            pressAnyKey.SetActive(false);
        }
    }

    IEnumerator LoadNewScene()
    {
        loadingScreen.SetActive(true);
        yield return new WaitForEndOfFrame();
        AsyncOperation async = SceneManager.LoadSceneAsync(firstScene, LoadSceneMode.Additive);
        async = SceneManager.LoadSceneAsync(secondScene, LoadSceneMode.Additive);
        async = SceneManager.LoadSceneAsync(playerScene, LoadSceneMode.Additive);
      //  Debug.Log("Started Load");

        while (!async.isDone)
        {
       //     Debug.Log("Loading...");
            yield return null;
        }
     //   Debug.Log("DONE");

        menuObject.SetActive(false);

    }
}
