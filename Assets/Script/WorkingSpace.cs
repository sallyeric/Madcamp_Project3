using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorkingSpace : MonoBehaviour
{
    #region Singleton
    public static WorkingSpace instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion

    public GameObject workingSpacePanel;       // inventory 창

    public Slot[] slots;                    // 인벤토리의 슬롯 리스트. (아이템이 아니라 슬롯!)
    public Transform slotHolder;            // slot들을 갖고 있는 놈. slotsParent.
    public PlayerMove player;

    public List<Item> selectedItems = new List<Item>();

    string[] stuffsList = {"분필", "열쇠", "간식", "날개", "필통", "간장", "간판"};

    bool isStuff;

    void Start()
    {
        slots = slotHolder.GetComponentsInChildren<Slot>();
        isStuff = false;
    }

    public bool SelectItem(Item _item)
    {
        Debug.Log("WorkingSpace/SelectItem");
        if (selectedItems.Count < 2)
        {
            selectedItems.Add(_item);
            UpdateSpace();

            if (selectedItems.Count == 2)
                MakeStuff();

            return true;
        }
        return false;
    }

    public void UnselectItem(Item _item)
    {
        if (selectedItems.Count == 2 && isStuff)
        {
            player.PutDownStuff();
            isStuff = false;
        }

        selectedItems.Remove(_item);
        UpdateSpace();
    }
    void UpdateSpace()
    {
        Debug.Log("WorkingSpace/UpdateSpace");

        /* 인벤토리 켜기/닫기 */
        if (selectedItems.Count != 0)
            workingSpacePanel.SetActive(true);
        else
            workingSpacePanel.SetActive(false);

        RedrawSlotUI();
    }

    void RedrawSlotUI()
    {
        Debug.Log("WorkingSpace/RedrawSlotUI");
        // 모든 슬롯 삭제
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].removeSlot();
        }

        // 아이템 개수만큼 처음부터 다시 그리기
        for (int i = 0; i < selectedItems.Count; i++)
        {
            slots[i].item = selectedItems[i];
            slots[i].UpdateSlotUI();
        }
    }

    void MakeStuff()
    {
        string stuffName = slots[0].item.itemName.ToString() + slots[1].item.itemName.ToString();
        Debug.Log("stuffName: " + stuffName);

        if (stuffsList.Contains(stuffName))
        {
            Debug.Log("존재하는 stuff!");
            player.RaiseStuff(stuffName);
            isStuff = true;
        } 
        else
        {
            Debug.Log("존재하지 않습니다.");
        }
    }
}
