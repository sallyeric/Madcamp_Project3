using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //public Text talkText;         //대화 텍스트  

    public TalkManager talkManager;
    public GameObject ChatBox;

    public GameObject StageMenu;
    public GameObject InGame;
    public GameObject Stage1;
    public GameObject Stage3;
    public Rigidbody2D rigid;
    public GameObject OptionCanvas;

    public GameObject Script_stage1;
    public GameObject Script_stage3;


    public bool isActive;      //지금 대화창이 나왔는지 여부? --> 스페이스바를 누르면 생기거나 없어지거나 한다.
    public int talkIndex;
    TextMeshPro textMeshPro;

    bool isGameStay = false;
    int clearStage = 0;

    string TAG = "GameManager/";

    private void Start()
    {
        isActive = false;
        textMeshPro = ChatBox.GetComponent<TextMeshPro>();
        talkIndex = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isGameStay)
        {
            OptionCanvas.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void ContinueButton()
    {
        OptionCanvas.SetActive(false);
        Time.timeScale = 1;
    }
    public void StopButton()
    {
        OptionCanvas.SetActive(false);
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void stageFirst()
    {
        StageMenu.SetActive(false);
        InGame.SetActive(true);
        Stage1.SetActive(true);
        rigid.gameObject.SetActive(true);
        Script_stage1.SetActive(true);
        isGameStay = true;
    }

    public void stageThird()
    {
        StageMenu.SetActive(false);
        InGame.SetActive(true);
        Stage3.SetActive(true);
        rigid.gameObject.SetActive(true);
        Script_stage3.SetActive(true);
        isGameStay = true;
    }

    public void Action(GameObject scanObj)
    {
        Debug.Log(TAG + "Action");
        OnTalk(scanObj.GetComponent<ObjectData>().id);

        ChatBox.SetActive(isActive);
    }

    void OnTalk(int id)
    {

        Debug.Log(TAG + "OnTalk");

        string talkData = talkManager.GetTalk(id, talkIndex);

        if (talkData == null)
        {
            isActive = false;
            talkIndex = 0;
            return;
        }

        textMeshPro.text = talkData;
        isActive = true;
        talkIndex++;
    }
}
