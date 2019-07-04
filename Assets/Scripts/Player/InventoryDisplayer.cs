using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryDisplayer : MonoBehaviour
{


    private PlayerInteract interaction;
    private Inventory inventory;
    private InGameTextReader reader;



    //set up in editor!
    public GameObject inventoryUI;
    public GameObject itemGrid;
    public GameObject inventorySlot;


    //array of the images
    private Image[] itemImages;
    private TextMeshProUGUI[] itemText;

    //how we should display empty inventory slots 
    public Sprite emptySprite;
    public string emptyString;


    public void Start()
    {

        interaction = GetComponent<PlayerInteract>();
        inventory = GetComponent<Inventory>();

        CreateInventoryUI();

        itemImages = itemGrid.GetComponentsInChildren<Image>();
        foreach (Image image in itemImages)
        {
            image.sprite = emptySprite;
        }

        itemText = itemGrid.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI text in itemText)
        {
            text.text = emptyString;
        }

        DisplayInventory();
        inventoryUI.SetActive(false);

    }

    public void CreateInventoryUI()
    {
        for (int i = 0; i <= inventory.MaxItemSlots; i++)
        {
            GameObject slot = Instantiate(inventorySlot, itemGrid.transform);
            Button button = slot.GetComponent<Button>();

            string tempInt = i.ToString();
            button.onClick.AddListener(() => SelectInventorySlot(tempInt));
        }

    }

    //turn on and off inventory menu
    public void ToggleInventoryMenu(bool isInventory)
    {
        inventoryUI.SetActive(isInventory);
        DisplayInventory();

    }

    //gets called by the ui button on click method - setup in create inventory ui
    public void SelectInventorySlot(string buttonIndex)
    {
        int selectedIndex = int.Parse(buttonIndex);
        if (inventory.itemSlots[selectedIndex].filled)
        {
            Debug.Log("you selected " + selectedIndex + " which corisponds to " + inventory.itemSlots[selectedIndex].item.name +
                " You have " + inventory.itemSlots[selectedIndex].quantity + " of this item ");

            Item selectedItem = inventory.itemSlots[selectedIndex].item;

            switch (selectedItem.type)
            {
                case ItemType.Book:
                    InGameText bookItem = (InGameText)selectedItem;
                    reader.DisplayText(bookItem);
                    break;

         
                case ItemType.Key:
                    Key keyItem = (Key)selectedItem;
               
                    break;
                case ItemType.Clue:
                    Clue clueItem = (Clue)selectedItem;
                    break;
                case ItemType.Film:
                    Film filmItem = (Film)selectedItem;
                    break;


            }

        }
        DisplayInventory();
    }

    //JUST update the visuals of the inventory- dont implement any inventory management stuff here please future danny
    public void DisplayInventory()
    {
        if (inventory.itemSlots.Count > 0)
        {
            for (int i = 0; i < inventory.MaxItemSlots; i++)
            {
                if (inventory.itemSlots[i].filled)
                {
                    itemImages[i].sprite = inventory.itemSlots[i].item.icon;


                    if (inventory.itemSlots[i].equiped)
                    {
                        var colors = itemImages[i].gameObject.GetComponent<Button>().colors;
                        colors.normalColor = Color.red;
                        colors.highlightedColor = Color.red;
                        itemImages[i].gameObject.GetComponent<Button>().colors = colors;
                        itemText[i].text = inventory.itemSlots[i].item.title + "(" + inventory.itemSlots[i].quantity + ")" + " Equipped ";
                        itemImages[i].gameObject.GetComponent<Button>().enabled = false;
                        itemImages[i].gameObject.GetComponent<Button>().enabled = true;

                    }
                    else
                    {
                        var colors = itemImages[i].gameObject.GetComponent<Button>().colors;
                        colors.normalColor = Color.white;
                        colors.highlightedColor = Color.white;
                        itemImages[i].gameObject.GetComponent<Button>().colors = colors;

                        itemImages[i].gameObject.GetComponent<Button>().enabled = false;
                        itemImages[i].gameObject.GetComponent<Button>().enabled = true;
                        itemText[i].text = inventory.itemSlots[i].item.title + "(" + inventory.itemSlots[i].quantity + ")";
                    }



                }
                else
                {
                    itemText[i].text = emptyString;
                    itemImages[i].sprite = emptySprite;

                }
            }
        }

    }


}
