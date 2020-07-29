using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using Debug = UnityEngine.Debug; // Diagnostics에 있는 거랑 충돌나서 이거 추가함 (영주 왈)

public class EventInStage1 : MonoBehaviour
{
    public Rigidbody2D rigid;
    public ItemManager itemManager;
    public GameManager manager;
    public PlayerMove player;
    public GameObject Scene1;
    public GameObject Scene2;
    public GameObject Scene31;
    public GameObject Scene32;
    public GameObject Scene33;
    public GameObject Scene4;
    public GameObject TrashCan;

    public GameObject FinishScreen;

    int trashCount = 0;
    Stopwatch stopwatch = new Stopwatch();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Portal12")
        {
            Debug.Log("portal");
            Scene1.SetActive(false);
            Scene2.SetActive(true);
            player.PlayerReposition(new Vector2(-7.5f, -2.8f));
        }

        if (collision.gameObject.name == "Portal21")
        {
            Debug.Log("portal");
            Scene2.SetActive(false);
            Scene1.SetActive(true);
            player.PlayerReposition(new Vector2(7.3f, -3f));
        }

        if (collision.gameObject.name == "Portal321")
        {
            Debug.Log("portal");
            Scene31.SetActive(false);
            Scene2.SetActive(true);
            player.PlayerReposition(new Vector2(-0.2f, -2.9f));
        }

        if (collision.gameObject.name == "Portal322")
        {
            Debug.Log("portal");
            Scene32.SetActive(false);
            Scene2.SetActive(true);
            player.PlayerReposition(new Vector2(3.9f, 1.1f));
        }

        if (collision.gameObject.name == "Portal323")
        {
            Debug.Log("portal");
            Scene33.SetActive(false);
            Scene2.SetActive(true);
            player.PlayerReposition(new Vector2(-4.1f, 1.1f));
        }

        if (collision.gameObject.name == "Portal24")
        {
            Debug.Log("portal");
            Scene2.SetActive(false);
            Scene4.SetActive(true);
            player.PlayerReposition(new Vector2(-2.7f, -2.7f));
        }

        if (collision.gameObject.name == "Portal42")
        {
            Debug.Log("portal");
            Scene4.SetActive(false);
            Scene2.SetActive(true);
            player.PlayerReposition(new Vector2(-7.7f, 3.0f));
        }

        if (collision.gameObject.name == "벌서는친구")
        {
            stopwatch.Reset();
            //시간을 재서 어느정도 시간이 흐르면 다음 Exit함수 실행하게...
            stopwatch.Start();
        }

        if (collision.gameObject.name == "Finish")
        {
            if (PlayerMove.stuffName == "날개")
            {
                Debug.Log("Finish!");
                Time.timeScale = 0;
                FinishScreen.SetActive(true);
            }
        }

        if (collision.gameObject.name == "GameOver")
        {
            rigid.transform.position = new Vector2(-2.7f, -2.7f);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "교실문 (1)" && Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("교실문1");
            Scene2.SetActive(false);
            Scene31.SetActive(true);
            player.PlayerReposition(new Vector2(-7.1f, -2.7f));
        }

        if (collision.gameObject.name == "교실문 (2)" && Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("교실문2");
            Scene2.SetActive(false);
            Scene32.SetActive(true);
            player.PlayerReposition(new Vector2(-7.1f, -2.7f));
        }

        if (collision.gameObject.name == "교실문 (3)" && Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("교실문3");
            Scene2.SetActive(false);
            Scene33.SetActive(true);
            player.PlayerReposition(new Vector2(-7.1f, -2.7f));
        }

        if (collision.gameObject.tag == "Trash" && Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("trash");
            Destroy(collision.gameObject);
            trashCount++;
            if (trashCount == 3)
                TrashCan.SetActive(true);
        }

        if (collision.gameObject.name == "벌서는친구")
        {
            if (Input.anyKey)
            {
                Debug.Log("~리셋~");
                stopwatch.Reset();
                stopwatch.Start();
            }

            // 5초가 지나면...
            if (stopwatch.ElapsedMilliseconds > 5000)
            {
                stopwatch.Reset();
                Debug.Log("10초 지남");
                manager.Action(collision.gameObject);

                rigid.constraints = RigidbodyConstraints2D.FreezePositionX |
                                    RigidbodyConstraints2D.FreezePositionY |
                                    RigidbodyConstraints2D.FreezeRotation;

                Debug.Log("GetTalk함수 끝");
            }

            if (Input.GetKeyDown(KeyCode.W) && manager.isActive)
            {
                manager.Action(collision.gameObject);
                if (!manager.isActive)
                {
                    rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
                    Item newItem = itemManager.GetWordItem(collision.gameObject.GetComponent<ObjectData>().id);
                    if (newItem != null)
                    {
                        Debug.Log("get newItem: " + newItem.itemName);
                        rigid.GetComponent<Inventory>().AddItem(newItem);
                    }
                }
            }
        }

        if (collision.gameObject.name == "칠판" && Input.GetKeyDown(KeyCode.W))
        {
            if (PlayerMove.stuffName == "분필")
            {
                manager.Action(collision.gameObject);

                // 말풍선 떠있으면 Freeze
                if (manager.isActive)
                    rigid.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                else
                {
                    // 대화가 끝나고 아이템 얻기
                    rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
                    Item newItem = itemManager.GetWordItem(collision.gameObject.GetComponent<ObjectData>().id);
                    if (newItem != null)
                    {
                        Debug.Log("get newItem: " + newItem.itemName);
                        rigid.GetComponent<Inventory>().AddItem(newItem);
                    }

                    collision.gameObject.SetActive(false);

                }
            }
        }

        if (collision.gameObject.name == "철창" && Input.GetKeyDown(KeyCode.W))
            if (PlayerMove.stuffName == "열쇠")
                collision.gameObject.SetActive(false);


        if (collision.gameObject.name == "고양이" && Input.GetKeyDown(KeyCode.W))
        {
            if (manager.isActive)
            {
                manager.Action(collision.gameObject);
                if (!manager.isActive)
                {
                    rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
                    Item newItem = itemManager.GetWordItem(collision.gameObject.GetComponent<ObjectData>().id);
                    if (newItem != null)
                    {
                        Debug.Log("get newItem: " + newItem.itemName);
                        rigid.GetComponent<Inventory>().AddItem(newItem);
                    }
                }
            }

            else if(PlayerMove.stuffName == "간식")
            {
                manager.Action(collision.gameObject);

                rigid.constraints = RigidbodyConstraints2D.FreezePositionX |
                                    RigidbodyConstraints2D.FreezePositionY |
                                    RigidbodyConstraints2D.FreezeRotation;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "벌서는친구")
        {
            stopwatch.Reset();
        }
    }
}
