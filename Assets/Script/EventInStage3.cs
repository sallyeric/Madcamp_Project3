using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EventInStage3 : MonoBehaviour
{
    public Rigidbody2D rigid;
    public ItemManager itemManager;
    public GameManager manager;
    public PlayerMove player;

    public GameObject Dark;
    public GameObject Scene1;
    public GameObject Scene2;
    public GameObject Scene3;

    public GameObject Portal_cancel;

    public GameObject FinishScreen;

    // 자물쇠 관련
    public NumberSystem TheNumber;
    public string correctNumber;

    bool isNumberActive = false;

    string TAG = "EventInStage3/";


    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.name)
        {
            case "Portal_cancel":
                manager.Action(collision.gameObject);
                break;

            case "Portal12":
                Debug.Log(TAG + "portal");
                Scene1.SetActive(false);
                Scene2.SetActive(true);
                player.PlayerReposition(new Vector2(16.6f, -9.7f));
                break;

            case "Portal21":
                Debug.Log(TAG + "portal");
                Scene2.SetActive(false);
                Scene1.SetActive(true);
                player.PlayerReposition(new Vector2(1f, -10.2f));
                break;

            case "Portal23":
                Debug.Log(TAG + "portal");
                Scene2.SetActive(false);
                Scene3.SetActive(true);
                player.PlayerReposition(new Vector2(5.9f, -9.5f));
                break;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Portal_cancel")
        {
            if (manager.isActive)
                manager.Action(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Portal32" && Input.GetKey(KeyCode.S))
        {
            Debug.Log(TAG + "portal");
            Scene3.SetActive(false);
            Scene2.SetActive(true);
            player.PlayerReposition(new Vector2(3.2f, -9.7f));
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            switch (collision.gameObject.name)
            {
                case "벽난로":
                    Debug.Log(TAG + "벽난로");
                    if (PlayerMove.stuffName == "장작")
                    {
                        Debug.Log(TAG + "장작");

                        manager.Action(collision.gameObject);
                        Dark.SetActive(false);
                        Portal_cancel.SetActive(false);
                        // 말풍선 떠있으면 Freeze
                        if (manager.isActive)
                            rigid.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                        else
                        {
                            // 대화 끝
                            rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
                            collision.gameObject.SetActive(false);
                        }
                    }
                    break;
                

                case "액자_1":
                    Debug.Log(TAG + "액자_1");

                    manager.Action(collision.gameObject);

                    if (manager.isActive)
                        rigid.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                    else
                    {
                        // 대화 끝
                        rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
                        Item newItem = itemManager.GetWordItem(collision.gameObject.GetComponent<ObjectData>().id);
                        if (newItem != null)
                        {
                            Debug.Log("get newItem: " + newItem.itemName);
                            rigid.GetComponent<Inventory>().AddItem(newItem);
                        }
                        collision.gameObject.SetActive(false);
                    }
                    break;

                case "캔버스":
                    Debug.Log(TAG + "캔버스");
                    if (PlayerMove.stuffName == "우산")
                    {
                        manager.Action(collision.gameObject);

                        if (manager.isActive)
                            rigid.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                        else
                        {
                            // 대화 끝
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
                    break;


                case "벽난로_불":
                    Debug.Log(TAG + "벽난로");
                    if (PlayerMove.stuffName == "편지")
                    {
                        Debug.Log("Finish!");
                        Time.timeScale = 0;

                        FinishScreen.SetActive(true);
                    }
                    break;

                case "편지함":
                    Debug.Log(TAG + "편지함");

                    if (PlayerMove.stuffName == "가위")
                    {
                        manager.Action(collision.gameObject);

                        if (manager.isActive)
                            rigid.constraints = RigidbodyConstraints2D.FreezePositionX | 
                                                RigidbodyConstraints2D.FreezePositionY | 
                                                RigidbodyConstraints2D.FreezeRotation;
                        else
                        {
                            // 대화 끝
                            rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
                            Item newItem = itemManager.GetWordItem(collision.gameObject.GetComponent<ObjectData>().id);
                            if (newItem != null)
                            {
                                Debug.Log("get newItem: " + newItem.itemName);
                                rigid.GetComponent<Inventory>().AddItem(newItem);
                            }
                        }
                    }
                    break;

                case "달력":
                    // 정답을 맞추지 못했었다면 비밀번호창 실행
                    if (!TheNumber.GetResult() && isNumberActive == false)
                    {
                        manager.Action(collision.gameObject);
                        if (manager.isActive)
                            rigid.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                        else
                        {
                            rigid.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                            StartCoroutine(ACoroutine());
                            isNumberActive = true;
                        }
                    }
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (collision.gameObject.name == "달력")
            {
                rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
                isNumberActive = false;
            }
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (collision.gameObject.name == "달력")
            {
                rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
                isNumberActive = false;
            }
        }

        if (TheNumber.GetResult())
        {
            Item newItem = itemManager.GetWordItem(collision.gameObject.GetComponent<ObjectData>().id);
            if (newItem != null)
            {
                Debug.Log("get newItem: " + newItem.itemName);
                rigid.GetComponent<Inventory>().AddItem(newItem);
            }
        }
    }

    IEnumerator ACoroutine()
    {
        TheNumber.ShowNumber(correctNumber);
        yield return new WaitUntil(() => !TheNumber.activated);
    }
}
