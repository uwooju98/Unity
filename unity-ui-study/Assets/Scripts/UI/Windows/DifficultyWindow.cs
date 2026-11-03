using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class DifficultyWindow : GenericWindow
{
    public enum Difficulty
    {
        None = -1,
        Easy,
        Nomal,
        Hard
    }

    // Player.Prefs.SetInt("Difficulty".(int)CurrentDifficulty);
    // base.Close();

    public Toggle[] toggles;

    private int SaveCount;

    public Difficulty CurrentDifficulty
    {
        get
        {
            for (int i = 0; i < toggles.Length; i++)
            {
                if (toggles[i].isOn)
                    return (Difficulty)i;

            }
            return Difficulty.None;
        }
        set
        {
            for (int i = 0; i < toggles.Length; i++)
            {
                toggles[i].SetIsOnWithoutNotify(i == (int)value);
            }
        }
    }



    private void Update()
    {
        KeyInput();
        Save();
        Exit();
    }

    public override void Open()
    {
        base.Open();

        CurrentDifficulty = (Difficulty)PlayerPrefs.GetInt("Difficulty", 0);
    }

    public override void Close()
    {
        if (CurrentDifficulty != Difficulty.None)
        {
            PlayerPrefs.SetInt("Difficulty", (int)CurrentDifficulty);
            PlayerPrefs.Save();
        }

        base.Close();
    }

    public void OnClickEasy(bool value)
    {
        CurrentDifficulty = Difficulty.Easy;
    }
    public void OnClickNomal(bool value)
    {
        CurrentDifficulty = Difficulty.Nomal;
    }
    public void OnClickHard(bool value)
    {
        CurrentDifficulty = Difficulty.Hard;
    }

    private void Save()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CurrentDifficulty == Difficulty.None) { return; }

            PlayerPrefs.SetInt("Difficulty", (int)CurrentDifficulty);
            PlayerPrefs.Save();

            Debug.Log(CurrentDifficulty);
        }
    }



    private void KeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            CurrentDifficulty = Difficulty.Easy;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            CurrentDifficulty = Difficulty.Nomal;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            CurrentDifficulty = Difficulty.Hard;

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            for(int i = 0; i < toggles.Length; i++)
            {
                if (toggles[i].isOn)
                {
                    if (i == toggles.Length - 1)
                    {
                        toggles[0].isOn = true;
                        break;
                    }
                    else
                    {
                        toggles[i + 1].isOn = true;
                        break;
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            for (int i = 0; i < toggles.Length; i++)
            {
                if (toggles[i].isOn)
                {
                    if (i == 0)
                    {
                        toggles[toggles.Length - 1].isOn = true;
                        break;
                    }
                    else
                    {
                        toggles[i - 1].isOn = true;
                        break;
                    }
                }
            }
        }
    }

    private void Exit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            windowManager.Open(Windows.Start);
    }
}

//esc 시작화면 이동
//화살표로 선택 바꾸기
//스페이스 누르면 저장하기
// 다시 창이 열리면 저장한 난이도를 알고있어야함