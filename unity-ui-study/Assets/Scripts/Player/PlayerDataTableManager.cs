using UnityEngine;
using System.Collections.Generic;

public static class PlayerDataTableManager
{
    private static readonly Dictionary<string, PlayerDataTable> tables =
        new Dictionary<string, PlayerDataTable>();

    public static PlayerStringTable playerStringTable => Get<PlayerStringTable>(DataTableIds.String);

    static PlayerDataTableManager()
    {
        Init();
    }

    private static void Init()
    {
#if UNITY_EDITOR
        foreach (var id in DataTableIds.StringTableIds)
        {
            var stringTable = new PlayerStringTable();
            stringTable.Load(id);
            tables.Add(id, stringTable);
        }
#else
        var stringTable = new StringTable();
        stringTable.Load(DataTableIds.String);
        tables.Add(DataTableIds.String, stringTable);
#endif

        var playersTable = new PlayersTable();
        playersTable.Load(PlayerDataTableIds.Player);
        tables.Add(PlayerDataTableIds.Player, playersTable);
    }


    public static T Get<T>(string id) where T : PlayerDataTable
    {
        if (!tables.ContainsKey(id))
        {
            Debug.Log("테이블 없음");
            return null;
        }

        return tables[id] as T;
    }

    public static PlayerStringTable StringTable => Get<PlayerStringTable>(PlayerDataTableIds.String);

    public static PlayerStringTable ItemTable => Get<PlayerStringTable>(PlayerDataTableIds.Player);

    public static void ChangeLanguage(Languages lang)
    {
        var newId = DataTableIds.StringTableIds[(int)lang];
        if (tables.ContainsKey(newId))
        {
            return;
        }

        string oldId = null;
        foreach (var id in DataTableIds.StringTableIds)
        {
            if (tables.ContainsKey(id))
            {
                oldId = id;
                break;
            }
        }

        if (oldId == null)
        {
            Debug.Log("활성화된 스트링 테이블 없음");
        }

        var table = tables[oldId] as PlayerStringTable;

        table.Load(newId);

        tables.Remove(oldId);

        tables.Add(newId, table);
    }
}
