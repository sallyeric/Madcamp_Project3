using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;

    void Start()
    {
        talkData = new Dictionary<int, string[]>();
        MakeData();
    }

    void MakeData()
    {
        talkData.Add(101, new string[] { "어디서 많이 본 동상이군." });     // 동상
        talkData.Add(102, new string[] { "학교 지도" });                    // 지도
        talkData.Add(103, new string[] { "어머낫" });                       // 창문
        talkData.Add(104, new string[] { "???" });                          // 칠판
        talkData.Add(105, new string[] { "쓰레기통" });                     // 쓰레기통
        talkData.Add(106, new string[] { "같이 서줘서 고마워.", "이제 가! 가버려!" });                       // 벌서는친구
        talkData.Add(107, new string[] { "간식이냐옹?", "고맙다옹" });
    }

    public string GetTalk(int id, int talkIndex)
    {
        Debug.Log("GetTalk: "+id+", " +talkIndex);
        Debug.Log(talkData[id]);
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }
}
