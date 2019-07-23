using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemEventType
{
    GetItem,
    LooseItem
}
[AddComponentMenu("GameEvents/Game Objective Event")]
public class ItemEvent : GameEventReceiver
{
    public ItemEventType itemEventType;
    public Item item;
    private Inventory inv;

    private void Start()
    {
        inv = GameObject.Find("Player").GetComponent<Inventory>();
    }


    public override void DoEvent()
    {
        switch (itemEventType)
        {
            case ItemEventType.GetItem:
                inv.AddItem(item);

                break;
            case ItemEventType.LooseItem:
                inv.RemoveItem(item);

                break;
        }

    }


}
