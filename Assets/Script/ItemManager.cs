using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    Dictionary<int, Item> itemData;
    
    void Start()
    {
        itemData = new Dictionary<int, Item>();
        MakeData();
    }

    void MakeData()
    {
        itemData.Add(101, new Item(ItemType.Word, "필"));    
        itemData.Add(102, new Item(ItemType.Word, "식"));
        itemData.Add(103, new Item(ItemType.Word, "사"));
        itemData.Add(104, new Item(ItemType.Word, "쇠"));    // 칠판
        itemData.Add(105, new Item(ItemType.Word, "개"));    // 쓰레기통
        itemData.Add(106, new Item(ItemType.Word, "열"));    // 벌서는친구
        itemData.Add(107, new Item(ItemType.Word, "날"));
    }

    public Item GetWordItem(int id)
    {
        Item result;

        try
        {
            result = itemData[id];
            itemData[id] = null;
            return result;
            
        } catch {
            return null;
        }
            
    }
}
