
using UnityEngine;
using static UnityEngine.Random;


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

    Sprite[] itemImages = { (Sprite)Resources.Load("WordItem/Item_word_blue", typeof(Sprite)),
    (Sprite) Resources.Load("WordItem/Item_word_orange", typeof(Sprite)),
    (Sprite) Resources.Load("WordItem/Item_word_pink", typeof(Sprite)),
    (Sprite) Resources.Load("WordItem/Item_word_yellow", typeof(Sprite))};

    public Item(ItemType _itemType, string _itemName)
    {
        itemType = _itemType;
        itemName = _itemName;
        itemImage = itemImages[Random.Range(0, 4)];
    }
}
