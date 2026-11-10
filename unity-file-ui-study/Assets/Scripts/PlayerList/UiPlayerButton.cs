using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiPlayerButton : MonoBehaviour
{
    public Image icon;
    public TMP_Text textName;
    public Button button;

    private SavePlrData data;

    public event Action<SavePlrData> OnClicked;

    private void Awake()
    {
        button.onClick.AddListener(OnClick);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(OnClick);
    }

    public void SetPlayer(SavePlrData player)
    {
        data = player;

        if (data == null || data.PlrData == null)
        {
            icon.sprite = null;
            textName.text = string.Empty;
            return;
        }

        icon.sprite = data.PlrData.SpriteIcon;
        textName.text = data.PlrData.StringName;
    }

    private void OnClick()
    {
        if (data == null)
        {
            return;
        }

        OnClicked?.Invoke(data);
    }
}