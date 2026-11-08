using System.Collections;
using TMPro;
using UnityEngine;

public class GameOverWindow : GenericWindow
{
    public TMP_Text leftStatsLabel;
    public TMP_Text leftStatValues;

    public TMP_Text rightStatsLabel;
    public TMP_Text rightStatValues;

    public TMP_Text textTotalScore;

    private int[] scores = new int[6];
    private int totalScore;
    private Coroutine routine;

    public override void Open()
    {
        base.Open();

        if (routine != null)
        {
            StopCoroutine(routine);
            routine = null;
        }

        OnReset();
        routine = StartCoroutine(CoPrint());
    }

    public override void Close()
    {
        if (routine != null)
        {
            StopCoroutine(routine);
            routine = null;
        }

        base.Close();
    }

    public void OnNext()
    {
        windowManager.Open(Windows.Start);
    }

    private void OnReset()
    {
        for (int i = 0; i < scores.Length; i++)
        {
            scores[i] = 0;
        }
        totalScore = 0;
    }

    private IEnumerator CoPrint()
    {
        leftStatsLabel.text = string.Empty;
        leftStatValues.text = string.Empty;
        rightStatsLabel.text = string.Empty;
        rightStatValues.text = string.Empty;
        textTotalScore.text = "000000000";

        for (int i = 0; i < scores.Length; i++)
        {
            yield return new WaitForSeconds(1f);

            scores[i] = Random.Range(0, 9999);

            TMP_Text label = i < scores.Length / 2 ? leftStatsLabel : rightStatsLabel;
            TMP_Text value = i < scores.Length / 2 ? leftStatValues : rightStatValues;

            label.text += $"STAT {(i % (scores.Length / 2)) + 1}\n";
            value.text += $"{scores[i]:D4}\n";

            totalScore += scores[i];
        }

        yield return new WaitForSeconds(1f);

        float elapsed = 0f;
        float duration = 2f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(elapsed / duration);
            int currentScore = Mathf.RoundToInt(Mathf.Lerp(0f, totalScore, t));

            textTotalScore.text = $"{currentScore:D9}";

            yield return null;
        }

        textTotalScore.text = $"{totalScore:D9}";
        routine = null;
    }
}
