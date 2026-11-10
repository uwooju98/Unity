using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UiItemButton : MonoBehaviour
{
    public Image icon;
    public TMP_Text textName;
    public Button button;

    private ItemData data;

    public void SetEmpty()
    {
        data = null;
        icon.sprite = null;
        textName.text = string.Empty;
    }

    public void SetItem(string itemId)
    {
        SetItem(DataTableManager.ItemTable.Get(itemId));
    }

    public void SetItem(ItemData data)
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
