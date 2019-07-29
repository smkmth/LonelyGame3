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
[System.Serializable]
public class Item : ScriptableObject {

    public Vector3 yposMenu;
    public GameObject itemMesh;
    public string title;
    public string description;
    public Sprite icon;
    public ItemType type;
	
}
