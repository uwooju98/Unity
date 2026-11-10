using System.Collections.Generic;
using UnityEngine;

public static class DataTableManager
{
    private static readonly Dictionary<string, DataTable> tables =
        new Dictionary<string, DataTable>();

    static DataTableManager()
    {
        Init();
    }

    private static void Init()
    {
#if UNITY_EDITOR
        foreach (var id in DataTableIds.StringTableIds)
        {
            var stringTable = new StringTable();
            stringTable.Load(id);
            tables.Add(id, stringTable);
        }
#else
        var stringTable = new StringTable();
        stringTable.Load(DataTableIds.String);
        tables.Add(DataTableIds.String, stringTable);
#endif

        var itemTable = new ItemTable();
        itemTable.Load(DataTableIds.Item);
        tables.Add(DataTableIds.Item, itemTable);
    }

    public static T Get<T>(string id)
        where T : DataTable
    {
        if (!tables.ContainsKey(id))
        {
            Debug.LogError("테이블 없음");
            return null;
        }
        return tables[id] as T;
    }

    public static StringTable StringTable => Get<StringTable>(DataTableIds.String);
    public static ItemTable ItemTable => Get<ItemTable>(DataTableIds.Item);
    public static PlrTable plrTable => Get<PlrTable>(DataTableIds.Plr);

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
            Debug.LogError("활성화된 스트링 테이블 없음");
        }

        var table = tables[oldId] as StringTable;
        table.Load(newId);
        tables.Remove(oldId);
        tables.Add(newId, table);
    }

}
