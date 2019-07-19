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

    public List<InGameText> collectedTextAssets;
    public int numberOfTextAssets;
    public void Start()
    {
        reader = GetComponent<InGameTextReader>();
    }

    public void ToggleTextDisplay(bool isTextDisplay)
    {
        textDisplayMenu.SetActive(isTextDisplay);
    }

    public void SelectTextItem(InGameText textItem)
    {
        reader.DisplayText(textItem);

    }

    public void AddTextAsset(InGameText textAsset)
    {
        collectedTextAssets.Add(textAsset);
        numberOfTextAssets++;
        GameObject slot = Instantiate(textDisplayItem, textDisplayGrid.transform);
        Button button = slot.GetComponent<Button>();
        Text buttontext = slot.GetComponentInChildren<Text>();
        buttontext.text = textAsset.title;
        button.onClick.AddListener(() => SelectTextItem(textAsset));

    }
}
