using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDataInfo : MonoBehaviour
{
    public TMP_Text textName;
    public Image icon;
    public TMP_Text texHP;
    public TMP_Text texAtk;
    public TMP_Text texDef;
    public TMP_Text texSpeed;

    private PlayerData data;

    private void SetEmpty()
    {
        data = null;
        textName.text = string.Empty;
        icon.sprite = null;
        texHP.text = string.Empty;
        texAtk.text = string.Empty;
        texDef.text = string.Empty;
        texSpeed.text = string.Empty;
    }


    public void SetItem(string id)
    {
        SetItem(PlayerDataTableManager.playerStringTable.Get(id));
    }
    public void SetItem(PlayerData data)
    {
        if (data == null)
        {
            SetEmpty();
            return;
        }

        this.data = data;
        textName.text = data.StringName;
        icon.sprite = data.SpriteIcon;
        texHP.text = data.HP.ToString();
        texAtk.text = data.Atk.ToString();
        texDef.text = data.Def.ToString();
        texSpeed.text = data.Speed.ToString();
    }
}
