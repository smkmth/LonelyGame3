using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterAssetList : MonoBehaviour
{

    public Item[] masterItemList;
    public GameObjective[] masterObjectiveList;
    public InGameText[] masterInGameTextList;
    
    public Item findItemByName(string itemName)
    {
        Item foundItem = null;


        foreach(Item item in masterItemList)
        {
            if (item.title == itemName)
            {
                foundItem = item;
                return foundItem;
            }
        }

        return foundItem;
   }
    public GameObjective findObjectiveByName(string objectiveName)
    {
        GameObjective foundObjective = null;


        foreach (GameObjective objective in masterObjectiveList)
        {
            if (objective.objectiveName == objectiveName)
            {
                foundObjective = objective;
                return foundObjective;

            }
        }

        return foundObjective;
    }


    public InGameText findTextByName(string textName)
    {
        InGameText foundText = null;


        foreach (InGameText text in masterInGameTextList)
        {
            if (text.title == textName)
            {
                foundText = text;
                return foundText;

            }
        }

        return foundText;
    }
}
