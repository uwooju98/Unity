using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoUI : MonoBehaviour
{
    [SerializeField] private TMP_Text idText;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private TMP_Text atkText;
    [SerializeField] private TMP_Text defText;
    [SerializeField] private TMP_Text speedText;

    [SerializeField] private Image characterIcon;

    public void SetInfo(PlayerData playerData)
    {
        LocalizationTable localizationTable = DataTableManager.GetTable<LocalizationTable>(DataTableManager.localization);

        idText.text = playerData.Id;
        nameText.text = localizationTable?.Get(playerData.Name) ?? playerData.Name;
        hpText.text = playerData.Hp.ToString();
        atkText.text = playerData.Atk.ToString();
        defText.text = playerData.Def.ToString();
        speedText.text = playerData.Speed.ToString();

        characterIcon.sprite = Resources.Load<Sprite>($"Icon/{playerData.Icon}");
    }
}
