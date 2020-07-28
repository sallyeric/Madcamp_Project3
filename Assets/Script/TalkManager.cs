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

        talkData.Add(200, new string[] { "어두워서 못가겠군." });
        talkData.Add(201, new string[] { "파리에서 붉은 장미와 함께 목걸이를 선물해줬던 것이 생각나는군.." });       // 장미
        talkData.Add(202, new string[] { "이제야 앞이 보여" });       // 벽난로
        talkData.Add(203, new string[] { "010-0701-4444번이 누구 번호더라 .." });       // 전화기
        talkData.Add(204, new string[] { "옥혜와 함께 했던 파리의 풍경이군! 직접 그려보고 싶어", "우산이 무지개색...이었나?"});      // 그림
        talkData.Add(205, new string[] { "군화가 걸려있네, 내 몸에 상처들과 상관이 있는 일일까?" });     // 군화
        talkData.Add(206, new string[] { "옥혜의 구두가 흙탕물에 아주 망가졌구먼 .." });                 // 구두
        talkData.Add(207, new string[] { "편지함이 잠겨 있네 어떻게 연담 ???" });                        // 편지함
        talkData.Add(208, new string[] { "비어있는 도화지가 있네.. 그림 실력을 좀 발휘해볼까?" });       // 캔버스
        talkData.Add(209, new string[] { "결혼 기념일이 언제였지?" });       // 달력
    }

    public string GetTalk(int id, int talkIndex)
    {
        Debug.Log("TalkManager/GetTalk: " + id+", " +talkIndex);
        
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }
}
