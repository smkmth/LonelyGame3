using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butts : MonoBehaviour
{
    public GameObject mainMenuObject;
    public GameObject settingsMenuObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BackToMainMenu()
    {
        mainMenuObject.SetActive(true);
        settingsMenuObject.SetActive(false);
    }

}
