using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/* Central
 * 인벤토리 관리하는 "Inventory" 오브젝트에 넣어준다.
 */

public class Central : MonoBehaviour
{
    public Transform invisibleSlot;     // 보이지 않는 여부 슬롯.

    Inventory inventory;
    List<Transform> slots;              // 슬롯 15개 리스트.

    void Start()
    {
        inventory = Inventory.instance;

        Debug.Log(transform.name);

        Transform slotsParent = transform.GetComponentInChildren<HorizontalLayoutGroup>().transform;

        Debug.Log("slotsParent name: " + slotsParent.name);

        slots = new List<Transform>();

        // 슬롯 15칸 전부에 대해서
        for (int i = 0; i < slotsParent.childCount; i++)
        {
            var slot = slotsParent.GetChild(i);
            slots.Add(slot);
        }
    }

    void BeginDrag(Transform slot)
    {
        Debug.Log("Central/BeginDrag: "+slot.name);

        // 드래그를 시작하면 우선 invisibleSlot과 바꿔서
        // 다른 slot들보다 맨 앞으로 놓이도록 한다.
        // (invisibleSlot은 미리 다른 slot보다 위 계층으로 놓아놓는다)
        invisibleSlot.gameObject.SetActive(true);
        SwapSlots(invisibleSlot, slot);

    }

    void EndDrag(Transform slot)
    {
        Debug.Log("Central/EndDrag: " + slot.name);
        Debug.Log("GetIndexByPosition: " + GetIndexByPosition(slot, invisibleSlot.GetSiblingIndex()));
        SwapSlots(invisibleSlot, slot);
        invisibleSlot.gameObject.SetActive(false);
    }

    /* SwapSlots(source: 출발지, destination: 목적지):
     * 
     * 두 슬롯의 위치를 바꾼다.
     */
    public static void SwapSlots(Transform sour, Transform dest)
    {
        Transform sourParent = sour.parent;
        Transform destParent = dest.parent;

        int sourIndex = sour.GetSiblingIndex();
        int destIndex = dest.GetSiblingIndex();

        sour.SetParent(destParent);
        sour.SetSiblingIndex(destIndex);

        dest.SetParent(sourParent);
        dest.SetSiblingIndex(sourIndex);
    }

    /* GetIndexByPosition()
     * x 좌표를 이용해서 슬롯을 drop한 곳의 index를 구한다.
     */
    public int GetIndexByPosition(Transform slot, int skipIndex = -1)
    {
        float slotWidth = slots[skipIndex + 2].position.x - slots[skipIndex + 1].position.x;
        Debug.Log("slotWidth: " + slotWidth);
        int result = inventory.items.Count-1;

        for (int i = inventory.items.Count-1; i >= 0; i--)
        {
            //Debug.Log("slot: " + slots[i].position.x);

            if (slot.position.x > slots[i].position.x)
                break;

            else if (skipIndex != i)
                result--;
        }
        //Debug.Log("slot: " + slot.position.x + " < " + slots[result].position.x);

        return result;

    }
}
