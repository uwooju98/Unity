using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioClip deathClip;
    public float jumpForce = 1000f;

    private int jumpCount = 0;
    private bool isGrounded = false;
    private bool isDead = false;

    private Rigidbody2D playerRigidbody;
    private Animator animator;
    private AudioSource playerAudio;


    private Vector2 playerSpawnPoint;
    private float speed = 10f;


    private BoxCollider2D slideCollider2D;
    private bool isSlide = false;


    private void Start()
    {
        slideCollider2D = GetComponent<BoxCollider2D>();
        playerSpawnPoint = new Vector2(transform.position.x, transform.position.y);
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Vector2.Distance(playerSpawnPoint, transform.position) > 1)
        {
            if (transform.position.y > playerSpawnPoint.y)
            {
                float targetX = Mathf.MoveTowards(transform.position.x, playerSpawnPoint.x, speed * Time.deltaTime);
                transform.position = new Vector2(targetX, transform.position.y);
            }
        }


        if (isDead)
        {
            return;
        }

        if (!isSlide)
        {
            if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
            {
                jumpCount++;

                playerRigidbody.linearVelocity = Vector2.zero;
                playerRigidbody.AddForce(new Vector2(0, jumpForce));

                playerAudio.Play();
            }
            else if (Input.GetKeyDown(KeyCode.Space) && playerRigidbody.linearVelocity.y > 0)
            {
                playerRigidbody.linearVelocity = playerRigidbody.linearVelocity * 0.5f;
            }
        }


        if (isGrounded)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                slideCollider2D.offset = new Vector2(0f, -0.32f);
                slideCollider2D.size = new Vector2(0.7f, 0.4f);
                isSlide = true;
                playerRigidbody.linearVelocity = Vector2.zero;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                slideCollider2D.offset = new Vector2(0f, -0.09f);
                slideCollider2D.size = new Vector2(0.5f, 0.8f);
                isSlide = false;
            }
        }


        animator.SetBool("Slid", isSlide);
        animator.SetBool("Grounded", isGrounded);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Dead" && !isDead)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.contacts[0].normal.y > 0.7f &&
            collision.collider.tag == "Platform")
        {
            Debug.Log("이상");
            isGrounded = true;
            jumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

    private void Die()
    {
        animator.SetTrigger("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play();

        playerRigidbody.linearVelocity = Vector2.zero;

        isDead = true;

        GameManager.instance.OnPlayerDead();
        Debug.Log("죽음");

    }


}
