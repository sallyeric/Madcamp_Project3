using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    //public Text talkText;         //대화 텍스트  

    public TalkManager talkManager;
    public GameObject ChatBox;

    public bool isActive;      //지금 대화창이 나왔는지 여부? --> 스페이스바를 누르면 생기거나 없어지거나 한다.
    public int talkIndex;
    TextMeshPro textMeshPro;

    private void Start()
    {
        isActive = false;
        textMeshPro = ChatBox.GetComponent<TextMeshPro>();
        talkIndex = 0;
    }

    public void Action(GameObject scanObj)
    {
        Debug.Log("Action!");
        OnTalk(scanObj.GetComponent<ObjectData>().id);

        ChatBox.SetActive(isActive);
    }

    void OnTalk(int id)
    {
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
