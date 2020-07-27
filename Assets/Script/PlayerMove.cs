using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 캐릭터 움직임 관련 */

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;                      // 최대 속력
    public float jumpPower;                     // 점프력
    public GameObject stuff;
    public GameManager manager;
    public ItemManager itemManager;

    Rigidbody2D rigid;                          // player
    SpriteRenderer spriteRenderer;              // player의 Flip을 위해
    bool facingRight = true;
    Animator animator;
    public SpriteRenderer stuffRenderer;

    public static string stuffName;
    Vector2 startPosition;

    GameObject scanObject;
    RaycastHit2D rayHit;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        stuffRenderer = stuff.GetComponent<SpriteRenderer>();
        stuffName = null;
        startPosition = rigid.transform.position;
        stuff.SetActive(false);
    }

    private void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(rigid.position);


        /* 얼굴 방향 바꾸기 */
        if (Input.GetButton("Horizontal"))
        {
            //Debug.Log(Input.GetButton("Horizontal"));

            // 왼쪽을 보고 있다가 오른쪽으로 바뀐 경우
            if (!facingRight && Input.GetAxisRaw("Horizontal") > 0)
            {
                spriteRenderer.flipX = !spriteRenderer.flipX;
                facingRight = true;
            }
            // 오른쪽 보고 있다가 왼쪽으로 바뀐 경우
            else if (facingRight && Input.GetAxisRaw("Horizontal") < 0)
            {
                spriteRenderer.flipX = !spriteRenderer.flipX;
                facingRight = false;
            }

            // 걷기 애니메이션 적용
            animator.SetBool("isWalking", true);
        }

        /* 미끄럼 없애기 */
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
            animator.SetBool("isWalking", false);
        }

        /* 점프 */
        if (Input.GetButtonDown("Jump") && animator.GetBool("isGround"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            animator.SetBool("isGround", false);
        }

        // Scan Object
        if (Input.GetKeyDown("w") && scanObject != null)
        {  
            // w키를 누르고, 스캔오브젝트가 널이 아니면 manager의 Action함수 실행
            Debug.Log("W누름: " + scanObject.name);
            manager.Action(scanObject);

            // 말풍선 떠있으면 Freeze
            if (manager.isActive)
                rigid.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            else
            {
                // 대화가 끝나고 아이템 얻기
                rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
                Item newItem = itemManager.GetWordItem(scanObject.GetComponent<ObjectData>().id);
                if (newItem != null){
                    Debug.Log("get newItem: " + newItem.itemName);
                    rigid.GetComponent<Inventory>().AddItem(newItem);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        float axisRaw = Input.GetAxisRaw("Horizontal");     // 오-왼 방향

        /* 걷기 */
        rigid.AddForce(Vector2.right * axisRaw, ForceMode2D.Impulse);

        /* 걷기: 속도 제한 */
        if (Mathf.Abs(rigid.velocity.x) > maxSpeed)
            rigid.velocity = new Vector2(maxSpeed * axisRaw, rigid.velocity.y);

        /* Ray */
        // Start에 만들면 안됨
        Debug.DrawRay(new Vector2(rigid.position.x - 0.1f, rigid.position.y), Vector2.right * 0.2f, new Color(0, 1, 0));    // 레이져를 그려준다
        rayHit = Physics2D.Raycast(new Vector2(rigid.position.x - 0.1f, rigid.position.y), Vector2.right, 0.2f, LayerMask.GetMask("Object"));                       // Object라는 레이어에 보인것들만 됨

        /* rayHit 충돌 확인 */
        if (rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject;
            //Debug.Log(scanObject.name);
        }
        else
            scanObject = null;
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if((contact.point.y - transform.position.y) < -0.7f)
                {
                    animator.SetBool("isGround", true);
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        /* 바닥에서 떨어질 때 */
        if (collision.gameObject.layer == 8)
            animator.SetBool("isGround", false);
    }

    public void RaiseStuff(string _stuffName)
    {
        stuffName = _stuffName;
        string path = "Stuffs/" + _stuffName;

        Sprite sprite = (Sprite) Resources.Load(path, typeof(Sprite));

        Debug.Log("stuff: " + stuffRenderer.gameObject.name);

        stuff.SetActive(true);
        stuffRenderer.sprite = Resources.Load(path, typeof(Sprite)) as Sprite;

        animator.SetBool("isStuff", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "GameOver")
            PlayerReposition(startPosition);
    }

    public void PlayerReposition(Vector2 position)
    {
        rigid.velocity = Vector2.zero;
        rigid.transform.position = position;

        if (!facingRight)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
            facingRight = true;
        }

    }

    public void PutDownStuff()
    {
        stuffName = null;
        stuff.SetActive(false);

        animator.SetBool("isStuff", false);
    }
}
