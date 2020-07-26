using System.Collections;
using System.Collections.Generic;
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
    bool activeWorkingSpace = false;            // 켜기/닫기 위한 bool

    Inventory inventory;

    public Slot[] slots;                    // 인벤토리의 슬롯 리스트. (아이템이 아니라 슬롯!)
    public Transform slotHolder;            // slot들을 갖고 있는 놈. slotsParent.

    public List<Item> selectedItems = new List<Item>();

    void Start()
    {
        inventory = Inventory.instance;
        slots = slotHolder.GetComponentsInChildren<Slot>();
    }

    public bool SelectItem(Item _item)
    {
        Debug.Log("WorkingSpace/SelectItem");
        if (selectedItems.Count < 2)
        {
            selectedItems.Add(_item);
            UpdateSpace();
            return true;
        }
        return false;
    }

    public void UnselectItem(Item _item)
    {
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
}
