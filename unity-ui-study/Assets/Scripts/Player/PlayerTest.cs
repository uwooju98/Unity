using UnityEngine;

public class PlayerCsvData
{
    public string Id { get; set; }
    public string String { get; set; }
}

public class PlayerTest : MonoBehaviour
{
    public PlayerSeletButtons[] playerSeletButtons;

    public PlayerDataInfo PlayerDataInfo;

    private void Start()
    {
        playerSeletButtons[0].SetItem("CHAR_1");
        playerSeletButtons[0].button.onClick.AddListener(() => PlayerDataInfo.SetItem("CHAR_1"));
        playerSeletButtons[1].SetItem("CHAR_2");
        playerSeletButtons[1].button.onClick.AddListener(() => PlayerDataInfo.SetItem("CHAR_2"));
        playerSeletButtons[2].SetItem("CHAR_3");
        playerSeletButtons[2].button.onClick.AddListener(() => PlayerDataInfo.SetItem("CHAR_3"));
        playerSeletButtons[3].SetItem("CHAR_4");
        playerSeletButtons[3].button.onClick.AddListener(() => PlayerDataInfo.SetItem("CHAR_4"));

    }
}
