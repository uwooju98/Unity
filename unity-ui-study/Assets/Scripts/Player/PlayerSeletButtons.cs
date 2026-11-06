using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerSeletButtons : MonoBehaviour
{
    public Image icon;
    public TMP_Text textName;
    public Button button;

    private PlayerData data;


    private void SetEmpty()
    {
        data = null;
        icon.sprite = null;
        textName.text = string.Empty;
    }

    public void SetItem(string itemId)
    {
        SetItem(PlayerDataTableManager.playerStringTable.Get(itemId));
    }

    public void SetItem(PlayerData data) // 호출시 필드 갱신
    {
        if (data == null)
        {
            SetEmpty();
            return;
        }
        this.data = data;
        icon.sprite = data.SpriteIcon;
        textName.text = data.StringName;
    }

}
