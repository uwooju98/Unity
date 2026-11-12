using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UiItemInfo2 : MonoBehaviour
{
    public static readonly string FormatString = "{0}: {1}";
    public Image icon;
    public TMP_Text textName;
    public TMP_Text textType;
    public TMP_Text textValue;
    public TMP_Text textCost;
    public TMP_Text textDesc;

    public void SetEmpty()
    {
        icon.sprite = null;
        icon.enabled = false;
        textName.text = string.Empty;
        textType.text = string.Empty;
        textValue.text = string.Empty;
        textCost.text = string.Empty;
        textDesc.text = string.Empty;
    }
    
    public void SetItem(SaveItemData saveItemData)
    {
        var st = DataTableManager.StringTable;
        var data = saveItemData.ItemData;
        icon.sprite = data.SpriteIcon;
        icon.enabled = true;
        textName.text = string.Format(FormatString, st.Get("NAME"), data.StringName);
        textType.text = string.Format(FormatString, st.Get("TYPE"), data.StringType);
        textValue.text = string.Format(FormatString, st.Get("VALUE"), data.Value);
        textCost.text = string.Format(FormatString, st.Get("COST"), data.Cost);
        textDesc.text = string.Format(FormatString, st.Get("DESC"), data.Desc);
    }
}
