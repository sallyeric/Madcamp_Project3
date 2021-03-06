﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/* InventoryUI
 * 갖고 있는 인벤토리 속 아이템들을
 * 보여주기 위한 클래스.
 */

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;       // inventory 창
    bool activeInventory = true;            // 켜기/닫기 위한 bool

    public Slot[] slots;                    // 인벤토리의 슬롯 리스트. (아이템이 아니라 슬롯!)
    public Transform slotHolder;            // slot들을 갖고 있는 놈. slotsParent.

    Inventory inventory;

    void Start()
    {
        inventoryPanel.SetActive(activeInventory);
        slots = slotHolder.GetComponentsInChildren<Slot>();     // slotHolder 안에 있는 slot item들 추출

        inventory = Inventory.instance;
        inventory.onChangeItem += RedrawSlotUI;
        inventory.onChangeItem += SlotChange;
    }

    void Update()
    {
        /* 인벤토리 켜기/닫기 */
        if (Input.GetKeyDown(KeyCode.I))
        {
            activeInventory = !activeInventory;
            inventoryPanel.SetActive(activeInventory);
        }
    }

    /* 아이템 갯수만큼 슬롯 활성화하기 */ 
    private void SlotChange(int itemNum)
    {
        for (int i=0; i<slots.Length; i++)
        {
            // 버튼(slotItem) 활성화/비활성화 하기
            if (i < itemNum) 
                slots[i].GetComponentInChildren<Button>().interactable = true;
            else 
                slots[i].GetComponentInChildren<Button>().interactable = false;
        }
    }

    void RedrawSlotUI(int itemNum)
    {
        // 모든 슬롯 삭제
        for(int i=0; i<slots.Length; i++)
        {
            slots[i].removeSlot();
        }

        // 아이템 개수만큼 처음부터 다시 그리기
        for (int i = 0; i < inventory.items.Count; i++)
        {
            slots[i].item = inventory.items[i];
            slots[i].UpdateSlotUI();
        }
    }
}
