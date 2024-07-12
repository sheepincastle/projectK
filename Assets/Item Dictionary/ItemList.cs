using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item{
    public string itemName;
    public Sprite icon;
    public int value;

    public Item(string name, Sprite icon, int value)
    {
     this.itemName = name;
     this.icon = icon;
     this.value = value;
    }
}
