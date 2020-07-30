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
        switch (collision.gameObject.name)
        {
            case "Portal12":
                Debug.Log("portal");
                Scene1.SetActive(false);
                Scene2.SetActive(true);
                player.PlayerReposition(new Vector2(-7.5f, -2.8f));
                break;

            case "Portal21":
                Debug.Log("portal");
                Scene2.SetActive(false);
                Scene1.SetActive(true);
                player.PlayerReposition(new Vector2(7.3f, -3f));
                break;


            case "Portal321":
                Debug.Log("portal");
                Scene31.SetActive(false);
                Scene2.SetActive(true);
                player.PlayerReposition(new Vector2(-0.2f, -2.9f));
                break;

            case "Portal322":
                Debug.Log("portal");
                Scene32.SetActive(false);
                Scene2.SetActive(true);
                player.PlayerReposition(new Vector2(3.9f, 1.1f));
                break;

            case "Portal323":
                Debug.Log("portal");
                Scene33.SetActive(false);
                Scene2.SetActive(true);
                player.PlayerReposition(new Vector2(-4.1f, 1.1f));
                break;
        

            case "Portal24":
                Debug.Log("portal");
                Scene2.SetActive(false);
                Scene4.SetActive(true);
                player.PlayerReposition(new Vector2(-2.7f, -2.7f));
                break;

            case "Portal42":
                Debug.Log("portal");
                Scene4.SetActive(false);
                Scene2.SetActive(true);
                player.PlayerReposition(new Vector2(-7.7f, 3.0f));
                break;

            case "벌서는친구":
                stopwatch.Reset();
                //시간을 재서 어느정도 시간이 흐르면 다음 Exit함수 실행하게...
                stopwatch.Start();
                break;

            case "Finish":
                if (PlayerMove.stuffName == "날개")
                {
                    Debug.Log("Finish!");
                    Time.timeScale = 0;
                    FinishScreen.SetActive(true);
                }
                break;
        }

        if (collision.gameObject.name == "GameOver")
        {
            rigid.transform.position = new Vector2(-2.7f, -2.7f);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            switch (collision.gameObject.name)
            {
                case "교실문 (1)":
                
                    Debug.Log("교실문1");
                    Scene2.SetActive(false);
                    Scene31.SetActive(true);
                    player.PlayerReposition(new Vector2(-7.1f, -2.7f));
                    break;

                case "교실문 (2)":
                
                    Debug.Log("교실문2");
                    Scene2.SetActive(false);
                    Scene32.SetActive(true);
                    player.PlayerReposition(new Vector2(-7.1f, -2.7f));
                    break;
                    

                case "교실문 (3)":
                
                    Debug.Log("교실문3");
                    Scene2.SetActive(false);
                    Scene33.SetActive(true);
                    player.PlayerReposition(new Vector2(-7.1f, -2.7f));
                    break;

                case "쓰레기":
                
                    Debug.Log("trash");
                    Destroy(collision.gameObject);
                    trashCount++;
                    if (trashCount == 3)
                        TrashCan.SetActive(true);
                    break;

                case "칠판":
                
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
                    else if (PlayerMove.stuffName == "필통")
                    {
                        manager.Action(150);

                        // 말풍선 떠있으면 Freeze
                        if (manager.isActive)
                            rigid.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                        else
                        {
                            rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
                        }
                    }
                    else if (PlayerMove.stuffName == "분식")
                    {
                        manager.Action(151);

                        // 말풍선 떠있으면 Freeze
                        if (manager.isActive)
                            rigid.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                        else
                        {
                            rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
                        }
                    }
                    break;

                case "철창":
                    if (PlayerMove.stuffName == "열쇠")
                        collision.gameObject.SetActive(false);
                    break;

                case "고양이":
                
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
                    else if (PlayerMove.stuffName == "간식")
                    {
                        manager.Action(collision.gameObject);

                        rigid.constraints = RigidbodyConstraints2D.FreezePositionX |
                                            RigidbodyConstraints2D.FreezePositionY |
                                            RigidbodyConstraints2D.FreezeRotation;
                    }
                    break;
            }
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
                Debug.Log("5초 지남");
                manager.Action(collision.gameObject);

                rigid.constraints = RigidbodyConstraints2D.FreezePositionX |
                                    RigidbodyConstraints2D.FreezePositionY |
                                    RigidbodyConstraints2D.FreezeRotation;
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
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "벌서는친구")
            stopwatch.Reset();
        
    }
}
