using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuDisplayer : MonoBehaviour
{
    public GameObject mainMenuScreen;

    public void ToggleMainMenuDisplay(bool isMainMenu)
    {
        mainMenuScreen.SetActive(isMainMenu);
    }
}
