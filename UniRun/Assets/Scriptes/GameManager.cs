using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static readonly string FormatScore = "SCORE: {0}";
    public TMP_Text scoreText;
    public GameObject gameOverUi;

    public bool isGameOver { get; private set; }

    private int score = 0;

    private void Start()
    {   
        SetScore(score);
        gameOverUi.SetActive(false);

    }

    private void Update()
    {
        if (isGameOver && Input.GetMouseButtonDown(0))
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

    public void SetScore(int score)
    {
        this.score = score;
        scoreText.text = string.Format(FormatScore, score);
    }

    public void AddScore(int add)
    {
        SetScore(score + add);
        if (isGameOver)
        {
            return;
        }
    }



    public void OnPlayerDead()
    {
        isGameOver = true;
        gameOverUi.SetActive(true);
    }
}
