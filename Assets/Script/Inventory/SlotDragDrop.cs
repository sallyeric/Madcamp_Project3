using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


/* SlotDragDrop
 * 인벤토리의 슬롯들을 드래그/드랍하기 위한 클래스.
 * slot 객체에 설정하면 되며, root는 
 */

public class SlotDragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IDropHandler, IEndDragHandler
{
    Transform root;

    void Start()
    {
        root = transform.root;      // transform의 가장 끝부분. (나의 경우 Canvas) 
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        root.BroadcastMessage("BeginDrag", transform, SendMessageOptions.DontRequireReceiver);      // Central.cs 에 있는 함수
    }

    public void OnDrag(PointerEventData eventData)
    {
        /* 드래그할 때 아이템도 마우스 따라 같이 움직이기 */
        transform.position = eventData.position;
    }

    public void OnDrop(PointerEventData eventData) {}

    public void OnEndDrag(PointerEventData eventData)
    {
        root.BroadcastMessage("EndDrag", transform, SendMessageOptions.DontRequireReceiver);        // Central.cs 에 있는 함수
    }
}
