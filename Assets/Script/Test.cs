using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Rigidbody2D rigid;
    private NumberSystem TheNumber;
    
    public string correctNumber;
    
    // Start is called before the first frame update
    void Start()
    {
        TheNumber = FindObjectOfType<NumberSystem>();
    }
    private void OnTriggerStay2D(Collider2D collision){

        if (Input.GetKeyDown(KeyCode.W))
        {
            // 정답을 맞추지 못했었다면 비밀번호창 실행
            if( !TheNumber.GetResult() ){
                rigid.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                StartCoroutine(ACoroutine());
            }
        }
    }
    IEnumerator ACoroutine(){

        TheNumber.ShowNumber(correctNumber);
        yield return new WaitUntil(()=> !TheNumber.activated);
    }
}
