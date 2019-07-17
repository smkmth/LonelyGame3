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
    SaveLoad
}
public class MenuManager : MonoBehaviour
{



    private InventoryDisplayer invDisplay;
    private TextDisplayer textDisplay;
    private PlayerMapDisplayer mapDisplay;
    private ObjectiveDisplayer objectiveDisplayer;

    public GameObject menu;
    public Button inventoryButton;
    public Button mapButton;
    public Button readingButton;
    public Button objectiveButton;
    public Button optionsButton;
    public Button saveLoadButton;

    private void Start()
    {
        invDisplay = GetComponent<InventoryDisplayer>();
        textDisplay = GetComponent<TextDisplayer>();
        mapDisplay = GetComponent<PlayerMapDisplayer>();
        objectiveDisplayer = GetComponent<ObjectiveDisplayer>();
        inventoryButton.onClick.AddListener(() => ToggleMenu(MenuType.Inventory));
        mapButton.onClick.AddListener(() => ToggleMenu(MenuType.Map));
        readingButton.onClick.AddListener(() => ToggleMenu(MenuType.Reading));
        objectiveButton.onClick.AddListener(() => ToggleMenu(MenuType.Objectives));
        optionsButton.onClick.AddListener(() => ToggleMenu(MenuType.Options));
        saveLoadButton.onClick.AddListener(() => ToggleMenu(MenuType.SaveLoad));


    }

    public void ToggleMenu(bool menuOn)
    {
        if (menuOn)
        {
            menu.SetActive(true);
            
        }
        else
        {
            menu.SetActive(false);

        }

    }


    public void ToggleMenu(MenuType chosenMenu)
    {
        switch (chosenMenu)
        {
            case MenuType.Inventory:
                objectiveDisplayer.ToggleObjectivesMenu(false);
                invDisplay.ToggleInventoryMenu(true);
                textDisplay.ToggleTextDisplay(false);
                mapDisplay.ToggleMap(false);
                Debug.Log("Inventory");
                break;
            case MenuType.Map:
                mapDisplay.ToggleMap(true);
                objectiveDisplayer.ToggleObjectivesMenu(false);
                invDisplay.ToggleInventoryMenu(false);
                textDisplay.ToggleTextDisplay(false);
                Debug.Log("map");
                break;
            case MenuType.Objectives:
                mapDisplay.ToggleMap(false);
                objectiveDisplayer.ToggleObjectivesMenu(true);
                invDisplay.ToggleInventoryMenu(false);
                textDisplay.ToggleTextDisplay(false);
                Debug.Log("objectives");
                break;
            case MenuType.Options:
                mapDisplay.ToggleMap(false);
                objectiveDisplayer.ToggleObjectivesMenu(false);
                invDisplay.ToggleInventoryMenu(false);
                textDisplay.ToggleTextDisplay(false);
                Debug.Log("options");
                break;
            case MenuType.Reading:
                mapDisplay.ToggleMap(false);
                objectiveDisplayer.ToggleObjectivesMenu(false);
                invDisplay.ToggleInventoryMenu(false);
                textDisplay.ToggleTextDisplay(true);
                Debug.Log("reading");
                break;
            case MenuType.SaveLoad:
                mapDisplay.ToggleMap(false);
                invDisplay.ToggleInventoryMenu(false);
                objectiveDisplayer.ToggleObjectivesMenu(false);
                Debug.Log("saveload");
                break;
        }
    }
}
