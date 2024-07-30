using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string itemName;
    public Sprite icon;
    public int value;
    public int power;
    public int defense;
    public int attackSpeed;
    public int duration;
    public ItemType itemType; // 추가된 필드

    public Item(string name, Sprite icon, int value, int power, int defense, int attackSpeed, int duration, ItemType itemType)
    {
        this.itemName = name;
        this.icon = icon;
        this.value = value;
        this.power = power;
        this.defense = defense;
        this.attackSpeed = attackSpeed;
        this.duration = duration;
        this.itemType = itemType; // 추가된 초기화 코드
    }
}

public enum ItemType
{
    Weapon,
    Armor,
    Potion
}


