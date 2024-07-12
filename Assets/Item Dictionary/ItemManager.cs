using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public List<Item> itemList; // 아이템을 추가할 리스트
    private Dictionary<string, Item> itemDictionary; // 아이템을 관리할 사전

    void Start()
    {
        itemDictionary = new Dictionary<string, Item>();

        // 리스트에 있는 아이템들을 사전에 추가
        foreach (Item item in itemList)
        {
            if (!itemDictionary.ContainsKey(item.itemName))
            {
                itemDictionary.Add(item.itemName, item);
            }
        }

        // 예제: 아이템 이름으로 아이템 검색
        string searchName = "Health Potion";
        if (itemDictionary.TryGetValue(searchName, out Item foundItem))
        {
            Debug.Log("Item found: " + foundItem.itemName + " Value: " + foundItem.value);
        }
        else
        {
            Debug.Log("Item not found: " + searchName);
        }
    }

    // 새로운 아이템을 추가하는 메서드
    public void AddItem(string itemName, Sprite icon, int value)
    {
        if (!itemDictionary.ContainsKey(itemName))
        {
            Item newItem = new Item(itemName, icon, value);
            itemList.Add(newItem);
            itemDictionary.Add(itemName, newItem);
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
        }
        else
        {
            Debug.Log("Item not found: " + itemName);
        }
    }
}

