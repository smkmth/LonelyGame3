using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum MainMenuButtonType
{
    NewGameButton,
    LoadGameButton,
    SettingsButton,
    QuitGameButton,
    AboutGameButton
}

public class MainMenu : MonoBehaviour
{
    public GameObject pressAnyKeyObject;
    public TextMeshProUGUI pressAnyKeyText;
    public float anyKeyPulseSpeed;

    public GameObject aboutGameObject;

    public GameObject loadingScreen;
    public GameObject menuObject;
    public GameObject mainMenuObject;
    public GameObject settingsMenuObject;
    public SettingsDisplayer settingsMenu;

    public Button newGameButton;
    public Button loadGameButton;
    public Button settingsButton;
    public Button aboutGameButton;
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
    [TextArea]
    public string aboutGameText;

    // Start is called before the first frame update
    void Start()
    {
        logo.SetActive(true);
        mainMenuObject.SetActive(true);
        settingsMenuObject.SetActive(false);
        aboutGameObject.SetActive(false);
        pressAnyKeyObject.SetActive(true);

        newGameButton.onClick.AddListener(() => ToggleMenu(MainMenuButtonType.NewGameButton));
        loadGameButton.onClick.AddListener(() => ToggleMenu(MainMenuButtonType.LoadGameButton));
        settingsButton.onClick.AddListener(() => ToggleMenu(MainMenuButtonType.SettingsButton));
        aboutGameButton.onClick.AddListener(() => ToggleMenu(MainMenuButtonType.AboutGameButton));
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
        else if (hover == "About")
        {
            explainationText.text = aboutGameText;

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
            case MainMenuButtonType.AboutGameButton:
                logo.SetActive(false);
                mainMenuObject.SetActive(false);
                aboutGameObject.SetActive(true);
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
        aboutGameObject.SetActive(false);
        logo.SetActive(true);
        mainMenuObject.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        if (pressAnyKeyObject.activeSelf)
        {
            pressAnyKeyText.color = new Color(pressAnyKeyText.color.r, pressAnyKeyText.color.g, pressAnyKeyText.color.b, Mathf.PingPong(Time.time * anyKeyPulseSpeed, 1));

        }

        if (Input.anyKey)
        {
            pressAnyKeyObject.SetActive(false);
        }
    }

    IEnumerator LoadNewScene()
    {
        loadingScreen.SetActive(true);
        yield return new WaitForEndOfFrame();
        AsyncOperation async = SceneManager.LoadSceneAsync(firstScene, LoadSceneMode.Additive);
        async = SceneManager.LoadSceneAsync(secondScene, LoadSceneMode.Additive);
        async = SceneManager.LoadSceneAsync(playerScene, LoadSceneMode.Additive);

        while (!async.isDone)
        {
            yield return null;
        }

        //after scene is loaded - wait for 2 seconds for the player to fall a bit - and everything to kind of shuffle in 
        yield return new WaitForSeconds(2.0f);
        menuObject.SetActive(false);

    }
}
