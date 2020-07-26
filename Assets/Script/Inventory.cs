using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    
    public List<Item> items = new List<Item>();

    // 슬롯 개수가 변경됨을 알려주기 위한 대리자(delegate)
    public delegate void OnSlotCountChange(int itemCount);
    public OnSlotCountChange onSlotCountChange;

    // 아이템 추가시 슬롯 UI에도 추가하기 위한 대리자
    //public delegate void OnChangeItem();
    //public OnChangeItem onChangeItem;

    public int slotCount;           // slot 최대 갯수

    void Start()
    {
        slotCount = 15;
    }

    // 슬롯에 빈공간 있으면 아이템 추가
    public bool AddItem(Item _item)
    {
        if(items.Count < slotCount)
        {
            items.Add(_item);
            //if (onChangeItem != null)
            //    onChangeItem.Invoke();
            return true;
        }
        return false;
    }

    // Field Item과 충돌한 상태에서 스페이스바로 획득하기
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.Space))
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
