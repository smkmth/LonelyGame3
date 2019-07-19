using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType
{
    Book,
    DestroyOnUse,
    EquipOnUse,
    NoUse
}
public class Item : ScriptableObject {

    public Mesh itemMesh;
    public string title;
    public string description;
    public Sprite icon;
    public ItemType type;
	
}
