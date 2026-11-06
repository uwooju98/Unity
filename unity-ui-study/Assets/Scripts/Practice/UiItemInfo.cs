using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiItemInfo : MonoBehaviour
{
    public TMP_Text textName;
    public Image icon;
    public TMP_Text texType;
    public TMP_Text texCost;
    public TMP_Text texValue;
    public TMP_Text texDesc;

    private ItemData data;

    private void SetEmpty()
    {
        data = null;
        textName.text = string.Empty;
        icon.sprite = null;
        texType.text = string.Empty;
        texCost.text = string.Empty;
        texValue.text = string.Empty;
        texDesc.text = string.Empty;
    }


    public void SetItem(string id)
    {
        SetItem(DataTableManager.ItemTable.Get(id));
    }
    public void SetItem(ItemData data)
    {
        if (data == null)
        {
            SetEmpty();
            return;
        }

        this.data = data;
        textName.text = data.StringName;
        icon.sprite = data.SpriteIcon;
        texType.text = data.Type.ToString();
        texCost.text = data.Cost.ToString();
        texValue.text = data.Value.ToString();
        texDesc.text = data.StringDesc;
    }
}
