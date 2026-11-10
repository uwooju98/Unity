using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemTable : DataTable
{
    private readonly Dictionary<string, ItemData> table = new Dictionary<string, ItemData>();

    public override void Load(string filename)
    {
        table.Clear();

        var path = string.Format(FormatPath, filename);
        var textAsset = Resources.Load<TextAsset>(path);
        var list = LoadCSV<ItemData>(textAsset.text);
        foreach (var item in list)
        {
            if (!table.ContainsKey(item.Id))
            {
                table.Add(item.Id, item);
            }
            else
            {
                Debug.LogError("아이템 아이디 중복");
            }
        }
    }

    public ItemData Get(string id)
    {
        if (!table.ContainsKey(id))
        {
            Debug.LogError($"{id}: 아이템 없음");
            return null;
        }
        return table[id];
    }

    public ItemData GetRandom()
    {
        var itemList = table.Values.ToList();
        return itemList[Random.Range(0, itemList.Count)];
    }
}
