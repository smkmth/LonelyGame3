using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextDisplayer: MonoBehaviour
{

    public GameObject textDisplayItem;
    public GameObject textDisplayMenu;
    public GameObject textDisplayGrid;
    public InGameTextReader reader;

    public List<InGameText> startingText;

    public List<InGameText> collectedTextAssets;
    public int numberOfTextAssets;
    public GameObject returnToMenuButton;
    public GameObject returnToGameButton;

    public void Start()
    {
        reader = GetComponent<InGameTextReader>();

        foreach(InGameText text in startingText)
        {
            AddTextAsset(text);
        }
    }

    public void ToggleTextDisplay(bool isTextDisplay)
    {
        textDisplayMenu.SetActive(isTextDisplay);
       
    }

    public void SelectTextItem(InGameText textItem)
    {
        reader.DisplayText(textItem, true);

    }

    public void AddTextAsset(InGameText textAsset)
    {
        collectedTextAssets.Add(textAsset);
        numberOfTextAssets++;
        GameObject slot = Instantiate(textDisplayItem, textDisplayGrid.transform);
        Button button = slot.GetComponent<Button>();
        slot.transform.GetChild(0).GetComponent<Text>().text = textAsset.title;
        slot.transform.GetChild(1).GetComponent<Text>().text = "";
        Text buttontext = slot.GetComponentInChildren<Text>();
        buttontext.text = textAsset.title;
        button.onClick.AddListener(() => SelectTextItem(textAsset));

    }
}
