using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiPlayerInfo : MonoBehaviour
{
    public Image icon;
    public TMP_Text textName;
    public TMP_Text textHp;
    public TMP_Text textAtk;
    public TMP_Text textDef;
    public TMP_Text textSpeed;

    private SavePlrData data;

    public void SetEmpty()
    {
        data = null;

        icon.sprite = null;
        textName.text = string.Empty;
        textHp.text = string.Empty;
        textAtk.text = string.Empty;
        textDef.text = string.Empty;
        textSpeed.text = string.Empty;
    }

    public void SetPlayer(SavePlrData player)
    {
        if (player == null || player.PlrData == null)
        {
            SetEmpty();
            return;
        }

        data = player;

        icon.sprite = data.PlrData.SpriteIcon;
        textName.text = data.PlrData.StringName;
        textHp.text = data.PlrData.Hp.ToString();
        textAtk.text = data.PlrData.Atk.ToString();
        textDef.text = data.PlrData.Def.ToString();
        textSpeed.text = data.PlrData.Speed.ToString();
    }
}