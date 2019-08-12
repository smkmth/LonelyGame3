using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextDisplayer : MonoBehaviour
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
    public GameObject upArrow;
    public GameObject downArrow;
    

    public void Start()
    {
        reader = GetComponent<InGameTextReader>();

        foreach (InGameText text in startingText)
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

    public void AddTextAsset(InGameText textAssetToAdd)
    {
        foreach (InGameText text in collectedTextAssets)
        {
            if (textAssetToAdd.title == text.title)
            {
                Debug.Log("already added " + text.title);
                return;
            }
        }
        collectedTextAssets.Add(textAssetToAdd);
        numberOfTextAssets++;
        GameObject slot = Instantiate(textDisplayItem, textDisplayGrid.transform);
        Button button = slot.GetComponent<Button>();
        slot.transform.GetChild(0).GetComponent<Text>().text = textAssetToAdd.title;
        slot.transform.GetChild(1).GetComponent<Text>().text = "";
        Text buttontext = slot.GetComponentInChildren<Text>();
        buttontext.text = textAssetToAdd.title;
        button.onClick.AddListener(() => SelectTextItem(textAssetToAdd));

    }
    public string[] GetTextList()
    {
        string[] text = new string[collectedTextAssets.Count];
        for (int i = 0; i < collectedTextAssets.Count; i++)
        {
            text[i] = collectedTextAssets[i].title;

        }

        return text;
    }

}
