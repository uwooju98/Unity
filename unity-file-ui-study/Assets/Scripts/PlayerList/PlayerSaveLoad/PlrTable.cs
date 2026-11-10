using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlrTable : DataTable
{
    private readonly Dictionary<string, PlrData> table = new Dictionary<string, PlrData>();

    public override void Load(string filename)
    {
        table.Clear();

        var path = string.Format(FormatPath, filename);
        var textAsset = Resources.Load<TextAsset>(path);
        var list = LoadCSV<PlrData>(textAsset.text);
        foreach (var plr in list)
        {
            if (!table.ContainsKey(plr.Id))
            {
                table.Add(plr.Id, plr);
            }
            else
            {
                Debug.LogError("플레이어 아이디 중복");
            }
        }
    }
    public PlrData Get(string id)
    {
        if (!table.ContainsKey(id))
        {
            Debug.LogError($"{id}: 플레이어 없음");
            return null;
        }
        return table[id];
    }

    public PlrData GetRandom()
    {
        var plrList = table.Values.ToList();
        return plrList[Random.Range(0, plrList.Count)];
    }
}
