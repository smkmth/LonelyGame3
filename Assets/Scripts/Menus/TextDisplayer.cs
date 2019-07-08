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

    // Start is called before the first frame update
    public void Start()
    {

        reader = GetComponent<InGameTextReader>();

    }
    public void ToggleTextDisplay(bool isTextDisplay)
    {
        textDisplayMenu.SetActive(isTextDisplay);
    }

    public void SelectTextItem(int textItemIndex)
    {
        reader.DisplayText(collectedTextAssets[(textItemIndex -1)]);

    }

    public void AddTextAsset(InGameText textAsset)
    {
        collectedTextAssets.Add(textAsset);
        numberOfTextAssets++;
        GameObject slot = Instantiate(textDisplayItem, textDisplayGrid.transform);
        Button button = slot.GetComponent<Button>();
        TextMeshProUGUI buttontext = slot.GetComponentInChildren<TextMeshProUGUI>();
        buttontext.text = textAsset.title;
        button.onClick.AddListener(() => SelectTextItem(numberOfTextAssets));

    }
}
