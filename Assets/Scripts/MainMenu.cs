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

    public GameObject menuObject;
    public GameObject mainMenuObject;
    public GameObject settingsMenuObject;

    public Button newGameButton;
    public Button loadGameButton;
    public Button settingsButton;
    public Button quitGameButton;

    public string playerScene;
    public string firstScene;
    public string secondScene;

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
                SceneManager.LoadScene(firstScene, LoadSceneMode.Additive);
                SceneManager.LoadScene(secondScene, LoadSceneMode.Additive);
                SceneManager.LoadScene(playerScene, LoadSceneMode.Additive);
                menuObject.SetActive(false); 

                break;
            case MainMenuButtonType.LoadGameButton:
                break;
            case MainMenuButtonType.SettingsButton:
                mainMenuObject.SetActive(false);
                settingsMenuObject.SetActive(true);
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
