using System;
using UnityEngine;

    // 1. 점프 뛰기 : 위로 속도를 가지게 한다 => Rigidbody 의 속도를 설정한다
    // 2. 애니메이션 제어
    // 3. 죽음 처리


public class PlayerController : MonoBehaviour
{

    private int jumpCount;

    private bool islive = true;

    public bool Islive() 
    {
        return islive;
    }

    

    [Header("점프 기능")]
    [SerializeField] Rigidbody2D rigid; // 유니티에서 직접 호출하는 방법
    [SerializeField] [Range(0, 20f)] float jumpSpeed;

    [Header("사운드")]

    [SerializeField] AudioSource jumpSound;

    [SerializeField] AudioSource dieSound;

    [SerializeField] AudioSource musicSound;

    [SerializeField] bool isGrounded;

    


    

    [Header("애니메이션")]

    [SerializeField] Animator animator;



    private void Start()
    {
        animator.SetBool("Grounded",false);     
    }

    // GetComponent = 소스코드 내에 있는 컴포넌트를 가져옴
    // 어트리뷰트 : 기능에 영향을 주지 않지만 유지보수에 편하라고 적용 시킨 것
    private void Awake() // Start보다 먼저 호출되는 준비 메세지 함수
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && jumpCount <= 1)
        {     
            Jump();
        }
    }


    private void Jump()
    {
        rigid.linearVelocityY = jumpSpeed;
        jumpSound.Play();
        jumpCount++;
    }


    private void Die()
    {
        islive = false;
        animator.SetTrigger("Die");
        rigid.linearVelocity = Vector2.zero;
        rigid.simulated = false;
        dieSound.Play(); 
    }



    private void OnCollisionEnter2D(Collision2D collision)
    { 
        if (collision.collider.tag == "Platform")
        {
            isGrounded = true;
            jumpCount = 0;
            animator.SetBool("Grounded",true);
            Debug.Log("닿았");
        }
  
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Platform")
        {
            isGrounded = false;
            animator.SetBool("Grounded",false);
            Debug.Log("떨어");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Dead")
        {
            Die();
            Debug.Log("죽음");
        }
    }

}
