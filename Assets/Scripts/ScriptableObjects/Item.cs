using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType
{
    Book,
    Key,
    Clue,
    Film,
    EndGameItem
  

}
public class Item : ScriptableObject {
    
    public string title;
    public string description;
    public Sprite icon;
    public ItemType type;
	
}
