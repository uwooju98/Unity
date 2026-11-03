using UnityEngine;

public class Scroller : MonoBehaviour
{
    private float speed = 9f;


    private void Awake()
    {
        
    }
    private void Update()
    {
        if (!GameManager.instance.isGameOver)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }   
    }
    


}
