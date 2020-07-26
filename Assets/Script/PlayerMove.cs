using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 캐릭터 움직임 관련 */

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;                      // 최대 속력
    public float jumpPower;                     // 점프력

    Rigidbody2D rigid;                          // player
    SpriteRenderer spriteRenderer;              // player의 Flip을 위해
    bool facingRight = true;
    Animator animator;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    private void FixedUpdate()
    {
        float axisRaw = Input.GetAxisRaw("Horizontal");     // 오-왼 방향

        /* 걷기 */
        rigid.AddForce(Vector2.right * axisRaw, ForceMode2D.Impulse);

        /* 걷기: 속도 제한 */
        if (Mathf.Abs(rigid.velocity.x) > maxSpeed)
            rigid.velocity = new Vector2(maxSpeed * axisRaw, rigid.velocity.y);

        
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
}
