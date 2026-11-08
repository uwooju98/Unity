using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private DataTableManager.Languages inspectorLanguage;


    public CharacterInfoUI characterInfoUI;

    private PlayersTable playersTable;
    private PlayerData currentPlayerData;


    private void Start()
    {
        playersTable = DataTableManager.GetTable<PlayersTable>(DataTableManager.player);
    }


    private void OnEnable()
    {
        DataTableManager.OnLanguageChanged += UpdateLanguage;
    }

    private void OnDisable()
    {
        DataTableManager.OnLanguageChanged -= UpdateLanguage;
    }


    private void UpdateLanguage()
    {
        if (currentPlayerData == null)
        {
            return;
        }

        characterInfoUI.SetInfo(currentPlayerData);
    }


    public void OnCharacterClick(string id)
    {
        PlayerData data = playersTable.Get(id);


        if (data == null)
            return;

        currentPlayerData = data;
        characterInfoUI.SetInfo(data);
    }

    public void ApplyInspectorLanguage()
    {
        DataTableManager.ChangeLanguage(inspectorLanguage);
    }
}
