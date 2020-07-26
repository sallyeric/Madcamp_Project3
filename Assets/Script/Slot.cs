using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item item;
    public Image itemIcon;

    // 아이템이 추가/교체된 경우
    // 슬롯 UI를 업데이트 한다.
    public void UpdateSlotUI()
    {
        itemIcon.sprite = item.itemImage;
        itemIcon.gameObject.SetActive(true);
    }

    // 아이템이 삭제된 경우
    // 아이템 내용을 삭제하고, 인벤토리에서 
    // 안 보이게 하기 위해 슬롯 오브젝트를 비활성화 
    public void removeSlot()
    {
        item = null;
        itemIcon.gameObject.SetActive(false);
    }
}
