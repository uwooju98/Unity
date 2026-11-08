using System.Collections.Generic;
using UnityEngine;


public static class DataTableManager
{
    public enum Languages
    {
        Korean,
        English,
        Japanese
    }

    public static readonly string player = "Player";
    public static readonly string characterTable = "CharacterTable";
    public static readonly string localization = "Localization";
    private static Languages currentLanguage = Languages.Korean;


    public static event System.Action OnLanguageChanged;


    private static Dictionary<string, DataTable> tables = new Dictionary<string, DataTable>();


    static DataTableManager()
    {
        Init();
    }


    private static void Init()
    {
        DataTable playerTable = new PlayersTable();
        playerTable.Load(characterTable);

        DataTable localizationTable = new LocalizationTable();
        localizationTable.Load(GetLanguage());

        tables.Add(player, playerTable);
        tables.Add(localization, localizationTable);
    }


    public static T GetTable<T>(string id) where T : DataTable
    {
        if (tables.TryGetValue(id, out DataTable data))
        {
             return data as T;
        }

        return null;
    }

    public static string GetLanguage()
    {
        return GetLanguage(currentLanguage);
    }

    public static string GetLanguage(Languages language)
    {
        return language switch
        {
            Languages.Korean => "StringTableKr",
            Languages.English => "StringTableEn",
            Languages.Japanese => "StringTableJp",
            _ => "StringTableKr"
        };
    }

    public static void ChangeLanguage(Languages language)
    {
        currentLanguage = language;

        LocalizationTable table = GetTable<LocalizationTable>(localization);

        if (table == null)
        {
            return;
        }

        table.Load(GetLanguage());

        OnLanguageChanged?.Invoke();
    }
}

