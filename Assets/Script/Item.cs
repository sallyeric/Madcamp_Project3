using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    Word,
    Product
}

[System.Serializable]
public class Item
{
    public ItemType itemType;
    public string itemName;
    public Sprite itemImage;
}
