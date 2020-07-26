using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Inventory
 * 실제 지니고 있는 인벤토리 속 아이템 데이터를
 * 저장하는 클래스.
 */

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;
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

    // 실제 갖고 있는 아이템들.
    public List<Item> items = new List<Item>();
    int slotCount = 15;

    public List<Item> selectedItems = new List<Item>();

    // 아이템 추가할 때 마다
    // 슬롯 UI에도 추가하기 위한 대리자(delegate).
    public delegate void OnChangeItem(int itemNum);
    public OnChangeItem onChangeItem;

    // 슬롯에 빈공간 있으면 아이템 추가
    public bool AddItem(Item _item)
    {
        if (items.Count < slotCount)
        {
            items.Add(_item);
            if (onChangeItem != null) onChangeItem.Invoke(items.Count);
            return true;
        }
        return false;
    }

    // Field Item과 충돌한 상태에서 스페이스바로 획득하기
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (collision.CompareTag("FieldItem"))
            {
                // fieldItem를 추가 -> 추가 성공하면 필드에서 제거.
                FieldItem fieldItem = collision.GetComponent<FieldItem>();
                if (AddItem(fieldItem.GetItem()))
                    fieldItem.DestroyItem();
            }

        }
    }
}
