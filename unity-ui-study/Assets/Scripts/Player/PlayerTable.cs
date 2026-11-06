using System.Collections.Generic;
using UnityEngine;

public class PlayersTable : PlayerDataTable
{
    private readonly Dictionary<string, PlayerData> table = new Dictionary<string, PlayerData>();

    public override void Load(string filename)
    {
        table.Clear();

        var path = string.Format(FormatPath, filename);
        var textAsset = Resources.Load<TextAsset>(path);
        var list = LoadCSV<PlayerData>(textAsset.text);

        foreach (var item in list)
        {
            if (!table.ContainsKey(item.Id))
            {
                table.Add(item.Id, item);
            }
            else
            {
                Debug.Log($"키 중복: {item.Id}");
            }
        }
    }

    public PlayerData Get(string id)
    {
        if (!table.ContainsKey(id))
        {
            Debug.Log($"{id}: 아이디 없음");
            return null;
        }

        return table[id];
    }
}
