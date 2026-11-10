using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UiItemSlot : MonoBehaviour // 외부에서 아이템 데이터를 넘겨주면 그것을 그려주는 역할
{   
    public int SlotIndex { get; set; } = -1;

    public Button button;
    public Image icon;
    public TMP_Text textItemName;


    public SaveItemData ItemData { get; private set; }

    public void SetEmpty()
    {
        ItemData = null;
        icon.sprite = null;
        textItemName.text = string.Empty;
    }

    public void SetItem(SaveItemData data)
    {
        ItemData = data;
        icon.sprite = ItemData.ItemData.SpriteIcon;
        textItemName.text = ItemData.ItemData.StringName;
    }
}

