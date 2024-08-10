using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public List<Item> itemList = new List<Item>(); // 아이템을 추가할 리스트
    private Dictionary<string, Item> itemDictionary = new Dictionary<string, Item>(); // 아이템을 관리할 사전

    void Start()
    {
        // 리스트에 있는 아이템들을 사전에 추가
        foreach (Item item in itemList)
        {
            if (!itemDictionary.ContainsKey(item.itemName))
            {
                itemDictionary.Add(item.itemName, item);
            }
        }
    }

    // 새로운 아이템을 추가하는 메서드
    public void AddItem(string itemName, Sprite icon, int value, int power, int defense, int attackSpeed, int duration, ItemType itemType)
    {
        if (!itemDictionary.ContainsKey(itemName))
        {
            Item newItem = new Item(itemName, icon, value, power, defense, attackSpeed, duration, itemType);
            itemList.Add(newItem);
            itemDictionary.Add(itemName, newItem);
            Debug.Log("Item added: " + itemName);
        }
        else
        {
            Debug.Log("Item already exists: " + itemName);
        }
    }

    // 아이템을 제거하는 메서드
    public void RemoveItem(string itemName)
    {
        if (itemDictionary.ContainsKey(itemName))
        {
            Item itemToRemove = itemDictionary[itemName];
            itemList.Remove(itemToRemove);
            itemDictionary.Remove(itemName);
            Debug.Log("Item removed: " + itemName);
        }
        else
        {
            Debug.Log("Item not found: " + itemName);
        }
    }

    // 아이템을 이름으로 검색하는 메서드
    public Item GetItem(string itemName)
    {
        if (itemDictionary.TryGetValue(itemName, out Item foundItem))
        {
            Debug.Log("Item found: " + foundItem.itemName + " Value: " + foundItem.value);
            return foundItem;
        }
        else
        {
            Debug.Log("Item not found: " + itemName);
            return null;
        }
    }

    // 아이템 리스트를 반환하는 메서드
    public List<Item> GetItemList()
    {
        return new List<Item>(itemList);
    }

    // 아이템을 타입별로 분류하는 메서드
    public void SortItemsByType(out List<Item> weapons, out List<Item> armors, out List<Item> potions)
    {
        weapons = new List<Item>();
        armors = new List<Item>();
        potions = new List<Item>();

        foreach (Item item in itemList)
        {
            switch (item.itemType)
            {
                case ItemType.Weapon:
                    weapons.Add(item);
                    break;
                case ItemType.Armor:
                    armors.Add(item);
                    break;
                case ItemType.Potion:
                    potions.Add(item);
                    break;
            }
        }
    }
}
