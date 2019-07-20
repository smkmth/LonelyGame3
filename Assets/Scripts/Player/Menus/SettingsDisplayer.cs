using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsDisplayer : MonoBehaviour
{

    public GameObject settingsMenu;

    public void ToggleSettingsDisplay(bool isSettingsMenu)
    {
        settingsMenu.SetActive(isSettingsMenu);
    }
}
