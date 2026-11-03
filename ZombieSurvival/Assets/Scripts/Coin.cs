using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour, IItem
{
    private GameManager gameManager;
    int score = 100;
    public UnityEvent OnUse { get; set; } = new UnityEvent();

    public void Start()
    {
        GameObject findGo = GameObject.FindWithTag("GameController");
        gameManager = findGo.GetComponent<GameManager>();
    }

    public void Use(GameObject target)
    {
        gameManager.AddScore(score);
        gameObject.SetActive(false);
        OnUse.Invoke();
    }
}