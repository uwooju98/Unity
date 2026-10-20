using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    private GameManager gameManager;
    private float speed = 10f;


    void Start()
    {
        rb = GetComponent<Rigidbody>();

        GameObject findGO = GameObject.FindWithTag("GameController");
        gameManager = findGO.GetComponent<GameManager>();
    }


    void Update()
    {
        float xInput = Input.GetAxis(("Horizontal"));
        float zInput = Input.GetAxis(("Vertical"));
        
        float xSpeed = xInput * speed;
        float zSpeed = zInput * speed;
        
        Vector3 newVelocity = new Vector3(xSpeed, 0f, zSpeed);

        rb.linearVelocity = newVelocity;
    }

    public void OnDie()
    {
        gameObject.SetActive(false);
        gameManager.EndGame();
    }

}
