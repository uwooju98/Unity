using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isGameOver = false;
    public TMP_Text scoreText;
    public GameObject gameOverUI;
    private int score = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (isGameOver && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void AddScore(int newScore)
    {
        if (!isGameOver)
        {
            score += newScore;
            scoreText.text = "Score : " + score;
        }
    }

    public void OnPlayerDead()
    {
        isGameOver = true;
        gameOverUI.SetActive(true);
    }

}
