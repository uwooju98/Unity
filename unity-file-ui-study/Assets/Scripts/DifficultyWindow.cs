using UnityEngine;
using UnityEngine.UI;

public class DifficultyWindow : GenericWindow
{
    public enum Difficulty
    {
        None = -1,
        Easy,
        Normal,
        Hard,
    }

    public Toggle[] toggles;

    public Difficulty CurrentDifficulty
    {
        get
        {
            for (int i = 0; i < toggles.Length; ++i)
            {
                if (toggles[i].isOn)
                {
                    return (Difficulty)i;
                }
            }
            return Difficulty.None;
        }
        set { toggles[(int)value].isOn = true; }
    }

    public override void Open()
    {
        CurrentDifficulty = (Difficulty)PlayerPrefs.GetInt("Difficulty", 1);

        base.Open();
    }

    public override void Close()
    {
        PlayerPrefs.SetInt("Difficulty", (int)CurrentDifficulty);
        base.Close();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerPrefs.SetInt("Difficulty", (int)CurrentDifficulty);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            manager.Open(Windows.Start);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            int index = (int)CurrentDifficulty;
            --index;
            if (index < 0)
            {
                index = toggles.Length - 1;
            }
            CurrentDifficulty = (Difficulty)index;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            int index = (int)CurrentDifficulty;
            index = (int)Mathf.Repeat(index + 1, toggles.Length);
            CurrentDifficulty = (Difficulty)index;
        }
    }

    public void OnClickEasy(bool value)
    {
        Debug.Log($"OnClickEasy: {value}");
    }

    public void OnClickNormal(bool value)
    {
        Debug.Log($"OnClickNormal: {value}");
    }

    public void OnClickHard(bool value)
    {
        Debug.Log($"OnClickHard: {value}");
    }
}
