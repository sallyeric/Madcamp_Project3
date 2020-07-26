using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 필드에 놓여진 아이템

public class FieldItem : MonoBehaviour
{
    public Item item;               // 어떤 아이템인가
    public SpriteRenderer image;    // 아이템에 맞는 이미지

    public void SetItem(Item _item)
    {
        item.itemType = _item.itemType;
        item.itemName = _item.itemName;
        item.itemImage = _item.itemImage;

        image.sprite = _item.itemImage;
    }

    public Item GetItem()
    {
        return item;
    }

    /* DestroyItem():
     * 아이템을 획득했을 경우 오브젝트 자체를 제거
     */
    public void DestroyItem()
    {
        Destroy(gameObject);
    }
}
