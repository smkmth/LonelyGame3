using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MenuType
{
    Inventory, 
    Map,
    Objectives,
    Reading,
    Options,
    MainMenu
}
public class MenuManager : MonoBehaviour
{
    public GameReset gameReset;
    public GameObject inGameUI;
    
    private InventoryDisplayer invDisplay;
    private TextDisplayer textDisplay;
    private PlayerMapDisplayer mapDisplay;
    private ObjectiveDisplayer objectiveDisplayer;
    private SettingsDisplayer settingsDisplayer;
    private MainMenuDisplayer mainMenuDispalyer;
    public GameObject menu;
    public Button inventoryButton;
    public Button mapButton;
    public Button readingButton;
    public Button objectiveButton;
    public Button optionsButton;
    public Button mainMenuButton;
    public GameObject sprintVingette;
    public MenuType currentMenu;
    private void Start()
    {
        gameReset = GameObject.Find("GameReset").GetComponent<GameReset>();

        invDisplay = GetComponent<InventoryDisplayer>();
        textDisplay = GetComponent<TextDisplayer>();
        mapDisplay = GetComponent<PlayerMapDisplayer>();
        objectiveDisplayer = GetComponent<ObjectiveDisplayer>();
        settingsDisplayer = GetComponent<SettingsDisplayer>();
        mainMenuDispalyer = GetComponent<MainMenuDisplayer>();

        inventoryButton.onClick.AddListener(() => ToggleMenu(MenuType.Inventory));
        mapButton.onClick.AddListener(() => ToggleMenu(MenuType.Map));
        readingButton.onClick.AddListener(() => ToggleMenu(MenuType.Reading));
        objectiveButton.onClick.AddListener(() => ToggleMenu(MenuType.Objectives));
        optionsButton.onClick.AddListener(() => ToggleMenu(MenuType.Options));
        mainMenuButton.onClick.AddListener(() => ToggleMenu(MenuType.MainMenu));
        ToggleMenu(false);


    }

    public void ToggleMenu(bool menuOn)
    {
        if (menuOn)
        {
            sprintVingette.SetActive(false);
            inGameUI.SetActive(false);
            menu.SetActive(true);
            objectiveDisplayer.ToggleObjectivesMenu(false);
            invDisplay.ToggleInventoryMenu(false);
            textDisplay.ToggleTextDisplay(false);
            mapDisplay.ToggleMap(false);
            settingsDisplayer.ToggleSettingsDisplay(false);
            mainMenuDispalyer.ToggleMainMenuDisplay(false);
        }
        else
        {
            sprintVingette.SetActive(true);

            inGameUI.SetActive(true);

            objectiveDisplayer.ToggleObjectivesMenu(false);
            invDisplay.ToggleInventoryMenu(false);
            textDisplay.ToggleTextDisplay(false);
            mapDisplay.ToggleMap(false);
            settingsDisplayer.ToggleSettingsDisplay(false);
            mainMenuDispalyer.ToggleMainMenuDisplay(false);
            menu.SetActive(false);
        }

    }

 
 
    public void ToggleMenu(MenuType chosenMenu)
    {
        
        menu.SetActive(true);
        currentMenu = chosenMenu;
        switch (chosenMenu)
        {
            case MenuType.Inventory:
                objectiveDisplayer.ToggleObjectivesMenu(false);
                invDisplay.ToggleInventoryMenu(true);
                textDisplay.ToggleTextDisplay(false);
                mapDisplay.ToggleMap(false);
                settingsDisplayer.ToggleSettingsDisplay(false);
                mainMenuDispalyer.ToggleMainMenuDisplay(false);
                Debug.Log("Inventory");
                break;
            case MenuType.Map:
                mapDisplay.ToggleMap(true);
                objectiveDisplayer.ToggleObjectivesMenu(false);
                invDisplay.ToggleInventoryMenu(false);
                textDisplay.ToggleTextDisplay(false);
                settingsDisplayer.ToggleSettingsDisplay(false);
                mainMenuDispalyer.ToggleMainMenuDisplay(false);

                Debug.Log("map");
                break;
            case MenuType.Objectives:
                mapDisplay.ToggleMap(false);
                objectiveDisplayer.ToggleObjectivesMenu(true);
                invDisplay.ToggleInventoryMenu(false);
                textDisplay.ToggleTextDisplay(false);
                settingsDisplayer.ToggleSettingsDisplay(false);
                mainMenuDispalyer.ToggleMainMenuDisplay(false);

                Debug.Log("objectives");
                break;
            case MenuType.Options:
                mapDisplay.ToggleMap(false);
                objectiveDisplayer.ToggleObjectivesMenu(false);
                invDisplay.ToggleInventoryMenu(false);
                textDisplay.ToggleTextDisplay(false);
                settingsDisplayer.ToggleSettingsDisplay(true);
                mainMenuDispalyer.ToggleMainMenuDisplay(false);

                Debug.Log("options");
                break;
            case MenuType.Reading:
                mapDisplay.ToggleMap(false);
                objectiveDisplayer.ToggleObjectivesMenu(false);
                invDisplay.ToggleInventoryMenu(false);
                textDisplay.ToggleTextDisplay(true);
                settingsDisplayer.ToggleSettingsDisplay(false);
                mainMenuDispalyer.ToggleMainMenuDisplay(false);


                Debug.Log("reading");
                break;
            case MenuType.MainMenu:
                gameReset.QuitGameToMenu();
                break;
        }
    }
}
