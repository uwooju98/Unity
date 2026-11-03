using System;
using UnityEngine;

    // 1. 점프 뛰기 : 위로 속도를 가지게 한다 => Rigidbody 의 속도를 설정한다
    // 2. 애니메이션 제어
    // 3. 죽음 처리


public class PlayerController : MonoBehaviour
{
    public AudioClip deathClip;

    private int jumpCount;

    private bool islive = true;

    public bool Islive() 
    {
        return islive;
    }

    

    [Header("점프 기능")]
     [SerializeField] private Rigidbody2D rigid; // 유니티에서 직접 호출하는 방법
    [SerializeField] [Range(0, 20f)] private float jumpSpeed;

    [Header("사운드")]

    [SerializeField] private AudioSource playerAudio;
    


    [SerializeField] private bool isGrounded;

    


    

    [Header("애니메이션")]

    [SerializeField] private Animator animator;


    private GameManager gameManager;

    private void Awake() // Start보다 먼저 호출되는 준비 메세지 함수
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        animator.SetBool("Grounded",false);     
        GameObject findGo = GameObject.FindWithTag("GameController");
        gameManager = findGo.GetComponent<GameManager>();


    }

    // GetComponent = 소스코드 내에 있는 컴포넌트를 가져옴
    // 어트리뷰트 : 기능에 영향을 주지 않지만 유지보수에 편하라고 적용 시킨 것


    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && jumpCount <= 2)
        {     
            Jump();
        }
    }


    private void Jump()
    {
        rigid.linearVelocityY = jumpSpeed;
        jumpCount++;
    }


    private void Die()
    {
        islive = false;
        animator.SetTrigger("Die");
        rigid.linearVelocity = Vector2.zero;
        rigid.simulated = false;

        gameManager.OnPlayerDead();
    }



    private void OnCollisionEnter2D(Collision2D collision)
    { 
        if (collision.collider.tag == "Platform" &&
            collision.contacts[0].normal.y > 0.7f)//
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
