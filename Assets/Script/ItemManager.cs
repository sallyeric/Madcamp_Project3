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

        itemData.Add(201, new Item(ItemType.Word, "장"));    // 장미
        itemData.Add(203, new Item(ItemType.Word, "가"));    // 전화시
        itemData.Add(204, new Item(ItemType.Word, "편"));    // 그림
        itemData.Add(205, new Item(ItemType.Word, "감"));    // 군화
        itemData.Add(206, new Item(ItemType.Word, "병"));    // 구두
        itemData.Add(207, new Item(ItemType.Word, "지"));    // 편지함
        itemData.Add(208, new Item(ItemType.Word, "위"));    // 캔버스
        itemData.Add(209, new Item(ItemType.Word, "우"));    // 달력
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
