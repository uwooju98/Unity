using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool isGameOver;
    public TextMeshProUGUI timeText;

    public GameObject gameOverUi;
    public TextMeshProUGUI recordText;

    private float surviveTime;


    private void Start()
    {
        surviveTime = 0f;
        isGameOver = false;
        gameOverUi.SetActive(false);
    }

    private void Update()
    {
        if (!isGameOver)
        {
            surviveTime += Time.deltaTime;
            timeText.text = $"TIME : {surviveTime:F1}";
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("Game");
            }
        }

    }

    public void EndGame()
    {
        isGameOver = true;
        gameOverUi.SetActive(true);

        float bestScore = PlayerPrefs.GetFloat("BestTime");
        if (surviveTime > bestScore)
        {
            bestScore = surviveTime;
            PlayerPrefs.SetFloat("BestTime",bestScore);
        }
        


        recordText.text = $"Best Record: {bestScore:F1}";
    }
}
